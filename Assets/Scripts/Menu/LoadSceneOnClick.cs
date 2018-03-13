using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public static string StartButtonText = "Start";

    void Start() {
        GameObject.FindGameObjectWithTag("StartButton").GetComponentInChildren<Text>().text = StartButtonText;
    }

    public void LoadByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
}
