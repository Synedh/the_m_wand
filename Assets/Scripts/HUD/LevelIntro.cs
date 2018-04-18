using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntro : MonoBehaviour {

    public GameObject LevelIntroUi;
    public GameObject SpawnEnemiesUI;
    
    bool doLerp;
    bool soundNotplayed;
    float startDuration;
    float totalDuration;

    // Use this for initialization
    void Start () {
        LevelIntroUi.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        if (ApplicationModel.level > 0)
            LevelIntroUi.GetComponentInChildren<Text>().text += ApplicationModel.level.ToString();
        else
            LevelIntroUi.GetComponentInChildren<Text>().text += "oo";
        SpawnEnemiesUI.SetActive(false);
        startDuration = 0f;
        totalDuration = 3f;
        soundNotplayed = true;
        doLerp = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (soundNotplayed)
        {
            Sound.sendSound("Sounds/StartLevel");
            soundNotplayed = false;
        }
        if (doLerp)
        {
            if (startDuration < totalDuration / 2)
            {
                startDuration += Time.deltaTime;
                LevelIntroUi.GetComponent<Image>().color = Color.Lerp(LevelIntroUi.GetComponent<Image>().color, new Color(0, 0, 0, 0), startDuration / totalDuration);
                LevelIntroUi.GetComponentInChildren<Text>().color = Color.Lerp(LevelIntroUi.GetComponentInChildren<Text>().color, new Color(1, 1, 1, 1), (startDuration / totalDuration) * 2);
            }
            else if (startDuration < totalDuration)
            {
                startDuration += Time.deltaTime;
                LevelIntroUi.GetComponent<Image>().color = Color.Lerp(LevelIntroUi.GetComponent<Image>().color, new Color(0, 0, 0, 0), startDuration / totalDuration);
                LevelIntroUi.GetComponentInChildren<Text>().color = Color.Lerp(LevelIntroUi.GetComponentInChildren<Text>().color, new Color(1, 1, 1, 0), (startDuration - totalDuration / 2) / totalDuration * 2);
            }
            else
            {
                doLerp = false;
                SpawnEnemiesUI.SetActive(true);
                LevelIntroUi.SetActive(false);
            }
        }

    }
}
