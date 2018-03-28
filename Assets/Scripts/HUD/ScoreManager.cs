using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public string scoreString;
    public static ScoreManager instance;
    
	void Start () {
        instance = this;
        scoreString = "00";
        GetComponent<Text>().text = "00";
	}

    public void addScore(int qty)
    {
        int score = Int32.Parse(scoreString);
        scoreString = (score + qty).ToString("D" + Math.Floor(Math.Log10(score + qty) + 2).ToString());
    }
	
	void Update () {
        GetComponent<Text>().text = scoreString;
    }
}
