using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class LoadSceneOnClick : MonoBehaviour
{
    public GameObject BlackPannel;

    List<Text> TextButtons = new List<Text>();
    bool doLerp;
    int sceneIndex;
    float startDuration;
    float totalDuration;

    void Start() {
        sceneIndex = 0;
        startDuration = 0;
        totalDuration = 2;
        BlackPannel.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            GameObject.FindGameObjectWithTag("ScoreText").GetComponentInChildren<Text>().text = "Score : " + ApplicationModel.score.ToString();
        }
    }

    public void LoadByIndex(int SceneIndex)
    {
        if (SceneIndex == 0)
            SceneManager.LoadScene(0);
        else
        {
            BlackPannel.SetActive(true);
            sceneIndex = SceneIndex;
            doLerp = true;
        }
    }

    public void SetLevel(int level) {
        ApplicationModel.level = level;
    }

    public void Update()
    {
        if (doLerp)
        {
            if (startDuration < totalDuration)
            {
                startDuration += Time.deltaTime;
                BlackPannel.GetComponent<Image>().color = Color.Lerp(BlackPannel.GetComponent<Image>().color, new Color(0, 0, 0, 1), startDuration / totalDuration);
            }
            else
            {
                doLerp = false;
                SceneManager.LoadScene(sceneIndex);

            }
        }
    }
}
