using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelScript : MonoBehaviour {


    public Level lvl;

	// Use this for initialization
	void Start () {

        this.lvl = new Level();
        lvl.addDialog();
        lvl.addWave();
        lvl.addWave();
        lvl.addWave();




        string json = JsonUtility.ToJson(lvl);

        print(json);

        AssetDatabase.CreateAsset(lvl, "Assets/Levels/testlevel12.asset");

    }
}
