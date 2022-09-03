using UnityEngine;

public class ColorSquare : MonoBehaviour
{

    private GameManager manager;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start() {
        manager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown() {
        manager.ColorClicked(spriteRenderer.color);
    }
}
