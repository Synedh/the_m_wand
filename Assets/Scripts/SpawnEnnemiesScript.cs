
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemiesScript : MonoBehaviour {

    public Character chara;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spanPoints;

	// Use this for initialization
	void Start () {
        Assets.Scripts.Fonctions.Tree.createTree();
        Assets.Scripts.Fonctions.Tree.displayTree();
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Spawn () {
		if (chara.CurrentLife <= 0f)
        {
            return;
        }

        Transform spawnPoint = spanPoints[Random.Range(0, spanPoints.Length)];
        //Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        GameObject currentBurst = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Characters/Ennemi"), spawnPoint.position, spawnPoint.rotation);
    }
}
