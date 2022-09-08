using Assets.Classes;
using UnityEngine;

public class NavigationButton : MonoBehaviour
{

    public NavigationDirection navigate = new NavigationDirection();

    SpriteRenderer myRenderer;
    GameColor gameColor;
    InstructionsDisplay instructionsDisplay;

    private void Start() {
        instructionsDisplay = FindObjectOfType<InstructionsDisplay>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameColor = new GameColor();
    }

    private void OnMouseEnter() {
        myRenderer.color = gameColor.GetBlue();
    }

    private void OnMouseExit() {
        myRenderer.color = new Color(1, 1, 1);
    }

    private void OnBecameInvisible() {
        myRenderer.color = new Color(1, 1, 1);
    }

    private void OnMouseDown() {
        if(navigate.ToString() == "Left") {
            instructionsDisplay.ScrollLeft();
        } else if (navigate.ToString() == "Right") {
            instructionsDisplay.ScrollRight();
        }
    }
}
