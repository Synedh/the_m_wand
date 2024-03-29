﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnnemiesScript : MonoBehaviour {

    public Character chara;
    public GameObject enemy;
    public float spawnTime;
    public int maxEnemies;
    public Transform[] spawnPoints;
    float spawnTimer;

	// Use this for initialization
	void Start () {
        // Assets.Scripts.Fonctions.Tree.displayTree();
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        spawnTimer = 0f;
	}

    void Update()
    {
        /* deprecated, see GameManager
        if (ApplicationModel.level == 0 || Int32.Parse(ScoreManager.instance.scoreString) < 50) {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnTime && GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies) {
                Spawn();
                spawnTimer = 0f;
            }
        }
        else if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            SceneManager.LoadScene(2);
        }*/ 
    }

    int generateDifficulty(int score)
    {
        if (score < 20)
            return 1;
        else if (score < 80)
            return new System.Random().Next(2) + 1;
        else if (score < 120)
            return 2;
        else if (score < 160)
            return new System.Random().Next(2) + 2;
        else
            return 3;
    }

	public EnnemiScript spawn() {
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        GameObject newObject = Instantiate(Resources.Load("Prefabs/Entities/Enemy"), spawnPoint.position, spawnPoint.rotation) as GameObject;
        EnnemiScript ennemi = newObject.GetComponent<EnnemiScript>();
        ennemi.getNodeByDiff(generateDifficulty(ApplicationModel.score));
        return ennemi;

    }


    public EnnemiScript spawn(GameObject enemyType, int spawn)
    {
        Transform spawnPoint;
        if (spawn == 0)
            spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        else
            spawnPoint = spawnPoints[spawn - 1];
        GameObject newObject = Instantiate(enemyType, spawnPoint.position, spawnPoint.rotation) as GameObject;
        EnnemiScript ennemi = newObject.GetComponent<EnnemiScript>();
        return ennemi;
    }
}
