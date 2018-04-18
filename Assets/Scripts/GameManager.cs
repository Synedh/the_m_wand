using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {




    //TODO spawn typical ennemi
    //TODO appear Boxes
    //TODO calcul score

    public SpawnEnnemiesScript spawner;
    public TextBoxes dialog;
    public Level level;
    public float spawnTime;


    private float spawnTimer = 0;
    private bool enemiesSpawned = false;
    private int nextDialog = 0;
    private Wave currentEvent;
    private int currentEventIndex = 0 ;



    // Use this for initialization
    void Start () {
        level = ApplicationModel.currentLevel;
        currentEvent = level.eventList[currentEventIndex];
        spawnTime = level.eventList[currentEventIndex].time;
    }

    void nextEvent()
    {
        if (currentEventIndex + 1 > level.eventList.Count && !dialog.isTextBoxActiv())
        {
            SceneManager.LoadScene(2);

        }
        else
        {
            currentEvent = level.eventList[++currentEventIndex];
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
        if ((enemiesSpawned && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) || spawnTimer > spawnTime )
            nextEvent();
    }
    
}
