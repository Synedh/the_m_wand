using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxes : MonoBehaviour {

    public GameObject OkTextBoxUI;
    public GameObject FctTextBoxUI;
    public GameObject swipe;
    public GameObject SpawnEnemiesUI;
    public string isWaitingForFunction;

    public static TextBoxes instance;

    private void Start()
    {
        isWaitingForFunction = null;
        instance = this;
        // SendOkBox("Now, use your spell on the first enemy on your path.");
        // SendFctBox("Let's start by drawing a square function to reduce this square root.", "square");
    }

    public void Resume()
    {
        Sound.loadSound("Sounds/click_button");
        OkTextBoxUI.SetActive(false);
        FctTextBoxUI.SetActive(false);
        swipe.SetActive(true);
        SpawnEnemiesUI.SetActive(true);
        isWaitingForFunction = null;
        Time.timeScale = 1f;
    }

    public void SendOkBox(string content)
    {
        OkTextBoxUI.SetActive(true);
        OkTextBoxUI.GetComponentInChildren<Text>().text = content;
        swipe.SetActive(false);
        Time.timeScale = 0f;
    }

    public void SendFctBox(string content, string functionName)
    {
        FctTextBoxUI.SetActive(true);
        SpawnEnemiesUI.SetActive(false);
        FctTextBoxUI.GetComponentInChildren<Text>().text = content;
        isWaitingForFunction = functionName;
    }

    public bool isTextBoxActiv()
    {
        return OkTextBoxUI.activeSelf || FctTextBoxUI.activeSelf;
    }
}
