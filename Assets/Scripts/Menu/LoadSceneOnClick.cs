using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class LoadSceneOnClick : MonoBehaviour
{
    float timer;
    List<Text> TextButtons = new List<Text>();

    void Start() {
        timer = 0;
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            TextButtons.Add(GameObject.FindGameObjectWithTag("StartButton").GetComponentInChildren<Text>());
            TextButtons.Add(GameObject.FindGameObjectWithTag("ExitButton").GetComponentInChildren<Text>());
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2) {
            GameObject.FindGameObjectWithTag("ScoreText").GetComponentInChildren<Text>().text = "Score : " + Int32.Parse(ScoreManager.instance.scoreString);
        }
    }

    public void LoadByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    private void SwitchTextButtonFontStyle(Text TextButton)
    {
        if (TextButton.fontStyle == FontStyle.Bold)
            TextButton.fontStyle = FontStyle.BoldAndItalic;
        else
            TextButton.fontStyle = FontStyle.Bold;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.2) {
            for (int i = 0; i < TextButtons.Count; ++i)
                SwitchTextButtonFontStyle(TextButtons[i]);
            timer = 0;
        }
    }
}
