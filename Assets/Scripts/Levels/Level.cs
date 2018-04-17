using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Level : ScriptableObject{
   
    public string goal;
    public int valueGoal;
    public List<Wave> eventList;

    public Level()
    {
        eventList = new List<Wave>();
        goal = "Ennemies";
        valueGoal = 20;
    }

    public void addWave()
    {
        eventList.Add(new Wave());
    }

    public void addDialog()
    {
        eventList.Add(new Wave("okBox"));
    }

    internal void deleteWave(int v)
    {
        eventList.RemoveAt(v);
    }

    internal void insertWave(int i)
    {
        eventList.Insert(i, new Wave());
    }
}


[Serializable]
public class Wave
{
    public string type;
    public bool show = false;
    public List<Dialog> dialogs;
    public List<Enemy> enemies;

    public Wave()
    {
        type = "Wave";
        enemies = new List<Enemy>();
        dialogs = new List<Dialog>();
    }

    public Wave(String type)
    {
        this.type = type;
    }

    internal void addEnemy()
    {
        enemies.Add(new Enemy());
    }

    internal void deleteEnemy(int j)
    {
        enemies.RemoveAt(j);
    }

    internal void addDialog()
    {
        dialogs.Add(new Dialog());
    }

    internal void deleteDialog(int j)
    {
        dialogs.RemoveAt(j);
    }
}

[Serializable]
public class Dialog
{
    public bool isFunction;
    public String functionName;
    public String content;
    

    public Dialog()
    {
        isFunction = false;
        functionName = "";
        content = "Contenu du pop-up";
    }

}


[Serializable]
public class Enemy
{
    public GameObject enemy;
    public int spawnpoint;
    public String function;
}