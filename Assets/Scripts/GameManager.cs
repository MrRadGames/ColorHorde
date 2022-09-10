using UnityEngine;
using System.Linq;
using Assets.Classes;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clickDisplay;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject restartCanvas;

    SquareItem[] grid;
    Color lastColor = new Color(1,1,1);
    GameColor gameColor;
    int clicks = 0;
    int rows = 14;
    int nextToLastColumnIndex;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        nextToLastColumnIndex = (rows*rows)-rows;

        gameColor = new GameColor();
        Color[] colors = gameColor.GetColorOptions();

        ColorSquare[] pickers = FindObjectsOfType<ColorSquare>().OrderBy(i => i.transform.GetSiblingIndex()).ToArray();
        for(int index = 0; index < pickers.Length; index++) {
            pickers[index].GetComponent<SpriteRenderer>().color = colors[index];
        }

        GameSquare[] squares = FindObjectsOfType<GameSquare>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();


        grid = squares.Select(square => new SquareItem(square.GetComponent<SpriteRenderer>(), false, false)).ToArray();
        SetupGame();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            Unpause();
        } else if(Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
    }

    private void Pause() {
        restartCanvas.SetActive(false);
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    public void Unpause() {
        restartCanvas.SetActive(true);
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void SetupGame() {
        if(isPaused) { return; }
        clicks = 0;
        updateClicksDisplay();

        foreach (SquareItem item in grid) {
            item.renderer.color = gameColor.GetRandomColor();
            item.captured = false;
            item.seen = false;
        }

        grid[0].captured = true;
        CaptureSquares(grid[0].renderer.color);
    }

    private void updateClicksDisplay() {
        clickDisplay.text = "Clicks: " + (clicks < 10 ? "0" : "") + clicks.ToString() + " / 25";
    }

    public void ColorClicked(Color color) {
        if (color.Equals(lastColor) || isPaused) { return; }
        clicks++;
        updateClicksDisplay();
        CaptureSquares(color);

    }

    private void CaptureSquares(Color color) {
        if(color.Equals(lastColor)) { return; }

        lastColor = color;
        CaptureSquareIfMatch(0, color);
        ResetSeenFlag();
        StartCoroutine(UpdateColors(color, 0));
    }

    private void CaptureSquareIfMatch(int index, Color color) {
        if(grid[index].seen) { return; }
        grid[index].seen = true;
        

        if (!grid[index].captured && !grid[index].renderer.color.Equals(color)) { return; }
        grid[index].captured = true;

        // Check above
        if(index % rows != 0) { CaptureSquareIfMatch(index-1, color); }
        // Check below
        if((index+1) % rows != 0) { CaptureSquareIfMatch(index+1, color); }
        // Check left
        if(index > rows-1) { CaptureSquareIfMatch(index-rows, color); }
        // Check right
        if(index < nextToLastColumnIndex) { CaptureSquareIfMatch(index+rows, color); }
    }

    private void ResetSeenFlag() {
        foreach (SquareItem item in grid) {
           item.seen = false;
        }
    }

    private IEnumerator UpdateColors(Color color, int startingIndex) {
        int lastSquareIndex = startingIndex + rows;
        for (int idx = startingIndex; idx < lastSquareIndex; idx++) { 
            if(idx % rows == 0 && idx < nextToLastColumnIndex) {
                StartCoroutine(UpdateColors(color, lastSquareIndex));
            }
            
            if(grid[idx].captured) {
                float elapsed = 0f;
                while(elapsed < 0.0014f) {
                    elapsed += Time.deltaTime;
                    yield return null;
                }
                grid[idx].renderer.color = color;
            }
        }
    }
}
