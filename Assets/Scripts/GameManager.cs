using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    

    public SpawnEnnemiesScript spawner;
    public TextBoxes dialog;
    public Level level;
    public int diff;
    public float spawnTime = 3f;


    private float spawnTimer = 0;
    private bool enemiesSpawned = false;
    private int nextDialog = 0;
    private Wave currentEvent;
    private int currentEventIndex = 0 ;


    void Start () {
        level = ApplicationModel.currentLevel;

        if (level != null)
        {
            currentEvent = level.eventList[currentEventIndex];
            spawnTime = level.eventList[currentEventIndex].time;
        }
        else
        {
            diff = ApplicationModel.level;
        }
    }

    void nextEvent()
    {
        currentEventIndex++;
        if (currentEventIndex >= level.eventList.Count && !dialog.isTextBoxActiv())
        {
            ApplicationModel.isWinned = true;
            SceneManager.LoadScene(2);
        }
        else 
        {
            currentEvent = level.eventList[currentEventIndex];
            enemiesSpawned = false;
            spawnTime = level.eventList[currentEventIndex].time;
            nextDialog = 0;
        }
    }

    private void spawnEnemi(Enemy e)
    {
        EnnemiScript enemy = spawner.spawn(e.enemy, e.spawnpoint);
        enemy.function = e.function;
    }



    // Update is called once per frame
    void Update () {
        if (level == null)
        {
            spawnTime = 1.5f;
            if (ApplicationModel.level == 0 || Int32.Parse(ScoreManager.instance.scoreString) < 50)
            {
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= spawnTime && GameObject.FindGameObjectsWithTag("Enemy").Length < 4)
                {
                    EnnemiScript enemy = spawner.spawn();
                    spawnTimer = 0f;
                }
            }
            else if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                SceneManager.LoadScene(2);

            }
        }
        else
        {
            if (!dialog.isTextBoxActiv() && (nextDialog < currentEvent.dialogs.Count))
            {
                Dialog currentDialog = currentEvent.dialogs[nextDialog];
                if (currentDialog.isFunction)
                    dialog.SendFctBox(currentDialog.content, currentDialog.functionName);
                else
                    dialog.SendOkBox(currentDialog.content);
                nextDialog++;
                spawnTimer = 0;
            }
            else if (!dialog.isTextBoxActiv() && enemiesSpawned == false)
            {
                spawnTimer = 0;
                foreach (Enemy e in currentEvent.enemies)
                    spawnEnemi(e);
                enemiesSpawned = true;
            }
            spawnTimer += Time.deltaTime;
            if ((enemiesSpawned && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) || spawnTimer > spawnTime)
                nextEvent();
        }
    }

    
}
