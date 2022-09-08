using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSquare : MonoBehaviour
{

    [SerializeField] float minWaitTime = 1f;
    [SerializeField] float maxWaitTime = 4f;
    [SerializeField] float lerpDuration = 1f;

    GameColor gameColor;
    SpriteRenderer spriteRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        gameColor = new GameColor();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = gameColor.GetRandomColor();
        StartCoroutine(ChangeColorLerp());

    }

    public IEnumerator ChangeColor() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            spriteRenderer.color = gameColor.GetRandomColor();
        }
    }

    public IEnumerator ChangeColorLerp() {
        float time;
        Color originalColor;
        Color newColor;
        while (true) {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            time = 0;
            originalColor = spriteRenderer.color;
            newColor = gameColor.GetRandomColor();
            while (time < lerpDuration) {
                spriteRenderer.color = Color.Lerp(originalColor, newColor, time / lerpDuration);
                time += Time.deltaTime;
                yield return null;
            }
            spriteRenderer.color = newColor;
        }
    }
}
