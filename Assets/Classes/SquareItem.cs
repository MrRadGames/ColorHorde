using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareItem
{
    public SpriteRenderer renderer;
    public bool seen;
    public bool captured;

    public SquareItem(SpriteRenderer renderer, bool seen, bool captured) {
        this.renderer = renderer;
        this.seen = seen;
        this.captured = captured;
    }
}
