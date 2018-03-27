
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemiesScript : MonoBehaviour {

    public Character chara;
    public GameObject enemy;
    public float spawnTime;
    public int maxEnemies;
    private float spawnTimer;
    public Transform[] spawnPoints;

	// Use this for initialization
	void Start () {
        Assets.Scripts.Fonctions.Tree.setLevel(ApplicationModel.level);
        Assets.Scripts.Fonctions.Tree.createTree();
        // Assets.Scripts.Fonctions.Tree.displayTree();
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        spawnTimer = 0f;
	}

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTime && GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
        {
            Spawn();
            spawnTimer = 0f;
        }
    }

    private int generateDifficulty(int score)
    {
        if (score < 10)
            return 1;
        else if (score < 40)
            return new System.Random().Next(2) + 1;
        else if (score < 60)
            return 2;
        else if (score < 80)
            return new System.Random().Next(2) + 2;
        else
            return 3;
    }
	
	// Update is called once per frame
	void Spawn () {
        Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        EnnemiScript.Create(generateDifficulty(Int32.Parse(ScoreManager.instance.scoreString)), spawnPoint);
    }
}
