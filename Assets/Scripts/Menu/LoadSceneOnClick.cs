using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Text txt;

    void Start() {
        txt = GetComponentInChildren<Text>();
    }

    public void LoadByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        txt.color = Color.red; //Or however you do your color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txt.color = Color.white; //Or however you do your color
    }
}
