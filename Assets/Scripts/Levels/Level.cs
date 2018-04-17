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
}


[Serializable]
public class Wave
{
    public string type;
    public List<Enemy> enemies;

    public Wave()
    {
        type = "Enemies";
        enemies = new List<Enemy>();
        enemies.Add(new Enemy());
        enemies.Add(new Enemy());
    }

    public Wave(String type)
    {
        this.type = type;
    }
}

[Serializable]
public class Enemy
{
    public int speed;
    public int attack;
    public int spawnpoint;
    public int depth;

    public Enemy()
    {
        speed = 1;
        attack = 1;
        spawnpoint = 0;
        depth = 1;
    }

}