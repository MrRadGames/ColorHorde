using System.Collections;
using UnityEngine;

namespace Assets.Classes {
    public class GameColor {

        private Color red = new Color(0.8490566f, 0.2683339f, 0.2683339f, 1f);
        private Color blue = new Color(0.3117657f, 0.38893f, 0.5849056f, 1f);
        private Color orange = new Color(0.8773585f, 0.5854089f, 0.2524475f, 1f);
        private Color purple = new Color(0.6499212f, 0.2257921f, 0.8113208f, 1f);
        private Color green = new Color(0.2980392f, 0.6431373f, 0.6431373f, 1f);
        private Color yellow = new Color(0.945098f, 0.8705882f, 0.3058824f, 1f);
        private Color[] colorOptions;
        
        public GameColor() {
            colorOptions = new Color[]{ red, blue, orange, purple, green, yellow};
        }

        public Color[] GetColorOptions() {
            return colorOptions;
        }

        public string ColorName(Color color) { 
            if(CompareColor(color, red)) { return "Red"; }
            if(CompareColor(color, blue)) { return "Blue"; }
            if(CompareColor(color, orange)) { return "Orange"; }
            if(CompareColor(color, purple)) { return "Purple"; }
            if(CompareColor(color, green)) { return "Green"; }
            if(CompareColor(color, yellow)) { return "Yellow"; }
            return "No match";
        }

        public Color GetBlue() {
            return blue;
        }

        public Color GetRandomColor() {
            int colorIndex = Random.Range(0, colorOptions.Length);
            return colorOptions[colorIndex];
        }

        public bool CompareColor(Color a, Color b) {
            return (a.r == b.r && a.g == b.g && a.r == b.r);
        }

    }
}