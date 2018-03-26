using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public static string startButtonText = "START";
    public static string titleText = "THE M . WAND";

    float timer;

    void Start() {
        GameObject.FindGameObjectWithTag("MainTitle").GetComponentInChildren<Text>().text = titleText;
        GameObject.FindGameObjectWithTag("StartButton").GetComponentInChildren<Text>().text = startButtonText;
        timer = 0;
    }

    public void LoadByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    private void SwitchMenuFontStyle()
    {
        if (GameObject.FindGameObjectWithTag("StartButton").GetComponentInChildren<Text>().fontStyle == FontStyle.Bold) {
            GameObject.FindGameObjectWithTag("StartButton").GetComponentInChildren<Text>().fontStyle = FontStyle.BoldAndItalic;
            GameObject.FindGameObjectWithTag("ExitButton").GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
        }
        else
        {
            GameObject.FindGameObjectWithTag("StartButton").GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
            GameObject.FindGameObjectWithTag("ExitButton").GetComponentInChildren<Text>().fontStyle = FontStyle.BoldAndItalic;
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.2) {
            SwitchMenuFontStyle();
            timer = 0;
        }
    }
}
