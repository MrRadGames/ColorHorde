using UnityEngine;
using System.Linq;
using Assets.Classes;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clickDisplay;

    SquareItem[] grid;
    Color lastColor = new Color(1,1,1);
    GameColor gameColor;
    int clicks = 0;

    // Start is called before the first frame update
    void Start()
    {
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

    public void SetupGame() {
        clicks = 0;
        updateClicksDisplay();

        Color[] colors = gameColor.GetColorOptions();
        foreach (SquareItem item in grid) {
            int colorIndex = Random.Range(0, 6);
            item.renderer.color = colors[colorIndex];
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
        if (color.Equals(lastColor)) { return; }
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
        if(index % 14 != 0) { CaptureSquareIfMatch(index-1, color); }
        // Check below
        if((index+1) % 14 != 0) { CaptureSquareIfMatch(index+1, color); }
        // Check left
        if(index > 13) { CaptureSquareIfMatch(index-14, color); }
        // Check right
        if(index < 182) { CaptureSquareIfMatch(index+14, color); }
    }

    private void ResetSeenFlag() {
        foreach (SquareItem item in grid) {
           item.seen = false;
        }
    }

    private IEnumerator UpdateColors(Color color, int startingIndex) {
        int lastSquareIndex = startingIndex + 14;
        for (int idx = startingIndex; idx < lastSquareIndex; idx++) { 
            if(idx % 14 == 0 && idx < 182) {
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
