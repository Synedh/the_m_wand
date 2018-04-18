using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {




    //TODO spawn typical ennemi
    //TODO appear Boxes
    //TODO calcul score

    public SpawnEnnemiesScript spawner;
    public TextBoxes dialog;
    public Level level;

    private bool next = true;
    private int nextDialog = 0;
    private Wave currentEvent;



    // Use this for initialization
    void Start () {
        level = ApplicationModel.currentLevel;
        currentEvent = level.eventList[0];
    }

    private void spawnEnemi(Enemy e)
    {
        EnnemiScript enemy = spawner.spawn(e.enemy, e.spawnpoint);
        enemy.difficulty = 1;//TODO ajouter la foonction a l'ennemy
    }

    // Update is called once per frame
    void Update () {
        if (next)
        {
            if (nextDialog < currentEvent.dialogs.Count)
            {
                Dialog currentDialog = currentEvent.dialogs[nextDialog];

                if (currentDialog.isFunction)
                    dialog.SendFctBox(currentDialog.content, currentDialog.functionName);
                else
                    dialog.SendOkBox(currentDialog.content);
            }
            next = false;




        }
	}
    
}
