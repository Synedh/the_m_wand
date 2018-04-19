using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTextBoxes : MonoBehaviour
{

    public GameObject OkTextBoxUI;

    public static MenuTextBoxes instance;

    private void Start()
    {
        instance = this;
        // SendOkBox("Now, use your spell on the first enemy on your path.");
        // SendFctBox("Let's start by drawing a square function to reduce this square root.", "square");
    }

    public void Resume()
    {
        Sound.loadSound("Sounds/click_button");
        OkTextBoxUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SendOkBox(string content)
    {
        OkTextBoxUI.SetActive(true);
        OkTextBoxUI.GetComponentInChildren<Text>().text = content;
        Time.timeScale = 0f;
    }

    public bool isTextBoxActiv()
    {
        return OkTextBoxUI.activeSelf;
    }
}
