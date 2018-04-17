using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public string scoreString;
    public static ScoreManager instance;
    GameObject score;
    bool doLerp;
    float startDuration;
    
	void Start () {
        score = this.gameObject;
        doLerp = false;
        startDuration = 0f;
        instance = this;
        scoreString = "00";
        ApplicationModel.score = 0;
        GetComponent<Text>().text = "00";
	}
	
	void Update () {
        GetComponent<Text>().text = scoreString;
        if (doLerp)
        {
            if (startDuration < 0.25f)
            {
                startDuration += Time.deltaTime;
                score.transform.localScale = Vector3.Lerp(score.transform.localScale, new Vector3(1.2f, 1.2f, 1.2f), startDuration * 4);
            }
            else if (startDuration < 0.5f)
            {
                startDuration += Time.deltaTime;
                score.transform.localScale = Vector3.Lerp(score.transform.localScale, new Vector3(1f, 1f, 1f), (startDuration - 0.25f) * 4);
            }
            else
            {
                doLerp = false;
                startDuration = 0;
            }
        }
    }

    public void addScore(int qty)
    {
        doLerp = true;
        int score = Int32.Parse(scoreString);
        ApplicationModel.score += qty;
        scoreString = (score + qty).ToString("D" + Math.Floor(Math.Log10(score + qty) + 2).ToString());
    }
}
