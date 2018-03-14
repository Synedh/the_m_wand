
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemiesScript : MonoBehaviour {

    public Character chara;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

	// Use this for initialization
	void Start () {
        Assets.Scripts.Fonctions.Tree.createTree();
        Assets.Scripts.Fonctions.Tree.displayTree();
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}

    private int generateDifficulty(int score)
    {
        if (score < 10)
            return 1;
        else if (score < 50)
            return new System.Random().Next(2) + 1;
        else
            return 2;
    }
	
	// Update is called once per frame
	void Spawn () {
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        EnnemiScript.Create(generateDifficulty(Int32.Parse(ScoreManager.instance.scoreString)), spawnPoint);
    }
}
