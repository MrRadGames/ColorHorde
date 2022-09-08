using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsDisplay : MonoBehaviour
{

    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;


    int panelIndex;


    // Start is called before the first frame update
    void Start()
    {
        panelIndex = 0;
    }

    public void ScrollRight() {
        leftButton.SetActive(true);
        int lastIndex = panelIndex;
        panelIndex++;
        if(panelIndex >= panels.Length-1) { rightButton.SetActive(false); }
        panels[lastIndex].SetActive(false);
        panels[panelIndex].SetActive(true);
    }

    public void ScrollLeft() {
        rightButton.SetActive(true);
        int lastIndex = panelIndex;
        panelIndex--;
        if (panelIndex <= 0) { leftButton.SetActive(false); }
        panels[lastIndex].SetActive(false);
        panels[panelIndex].SetActive(true);
    }

    
}
