using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    float timer;
    List<Text> TextButtons = new List<Text>();

    // Use this for initialization
    void Start () {
        timer = 0;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            TextButtons.Add(GameObject.FindGameObjectWithTag("StartButton").GetComponentInChildren<Text>());
            TextButtons.Add(GameObject.FindGameObjectWithTag("ExitButton").GetComponentInChildren<Text>());
        }
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer > 0.2)
        {
            for (int i = 0; i < TextButtons.Count; ++i)
                SwitchTextButtonFontStyle(TextButtons[i]);
            timer = 0;
        }
    }

    private void SwitchTextButtonFontStyle(Text TextButton)
    {
        if (TextButton.fontStyle == FontStyle.Bold)
            TextButton.fontStyle = FontStyle.BoldAndItalic;
        else
            TextButton.fontStyle = FontStyle.Bold;
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
