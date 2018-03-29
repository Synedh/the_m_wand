using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public string scoreString;
    public static ScoreManager instance;
    public int difficulty;
    
	void Start () {
        instance = this;
        scoreString = "00";
        difficulty = 0;
        GetComponent<Text>().text = "00";
	}
	
	void Update () {
        GetComponent<Text>().text = scoreString;
    }

    public void addScore(int qty)
    {
        int score = Int32.Parse(scoreString);
        scoreString = (score + qty).ToString("D" + Math.Floor(Math.Log10(score + qty) + 2).ToString());
        UpdateDifficulty();
    }

    public void UpdateDifficulty()
    {
        int score = Int32.Parse(scoreString);
        if (score < 10)
            difficulty = 1;
        else if (score < 40)
            difficulty = new System.Random().Next(2) + 1;
        else if (score < 60)
            difficulty = 2;
        else if (score < 80)
            difficulty = new System.Random().Next(2) + 2;
        else
            difficulty = 3;
    }
}
