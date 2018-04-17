using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : EditorWindow
{

    public Level level;
    public Vector2 scrollPosition;

    [MenuItem("Window/Level Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(LevelEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            level = AssetDatabase.LoadAssetAtPath(objectPath, typeof(LevelEditor)) as Level;
        }

    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Level Editor", EditorStyles.boldLabel);
        if (level != null)
        {
            if (GUILayout.Button("Show Level"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = level;
            }
        }
        if (GUILayout.Button("Open Level"))
        {
            OpenLevel();
        }
        if (GUILayout.Button("New Level"))
        {
            EditorUtility.FocusProjectWindow();
            level = CreateLevel.Create();
            Selection.activeObject = level;
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(20);


        if (level != null)
        {
            level.goal = EditorGUILayout.TextField("Level goal", level.goal as string);
            level.valueGoal = EditorGUILayout.IntField("Goal value", level.valueGoal);
            
            GUILayout.Label("Events : ");
            if (GUILayout.Button("Add Event", GUILayout.ExpandWidth(false)))
            {
                level.addWave();
            }
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);


            for (int i = 0; i < level.eventList.Count; i++)
            {
                Rect rectEvent = EditorGUILayout.BeginVertical();
                rectEvent.x += 5;
                GUI.Box(rectEvent, GUIContent.none);
                GUILayout.BeginHorizontal();
                GUILayout.Space(5);
                GUILayout.Label("Wave " + (i + 1) + " :");
                level.eventList[i].show = ((bool)EditorGUILayout.Toggle("Show", level.eventList[i].show, GUILayout.ExpandWidth(false)));
                
                if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false)))
                {
                    level.deleteWave(i);
                    continue;
                }
                if (GUILayout.Button("Insert", GUILayout.ExpandWidth(false)))
                {
                    level.insertWave(i);
                }
                GUILayout.EndHorizontal();
                if (!level.eventList[i].show)
                {
                    EditorGUILayout.EndVertical();
                    continue;
                }
                GUILayout.BeginHorizontal();
                GUILayout.Space(5);
                if (GUILayout.Button("Add Dialog", GUILayout.ExpandWidth(false)))
                {
                    level.eventList[i].addDialog();
                }
                GUILayout.EndHorizontal();

                for (int j = 0; j < level.eventList[i].dialogs.Count; j++)
                {
                    Rect rect = EditorGUILayout.BeginVertical();
                    rect.x += 10;
                    GUI.Box(rect, GUIContent.none);
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    GUILayout.Label("Dialog " + (j + 1) + " :");
                    if (GUILayout.Button("Delete Dialog", GUILayout.ExpandWidth(false)))
                    {
                        level.eventList[i].deleteDialog(j);
                        continue;
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    level.eventList[i].dialogs[j].isFunction = (bool)EditorGUILayout.Toggle("Is a function dialog", level.eventList[i].dialogs[j].isFunction, GUILayout.ExpandWidth(false));
                    if (level.eventList[i].dialogs[j].isFunction)
                        level.eventList[i].dialogs[j].functionName = EditorGUILayout.TextField("Function's name", level.eventList[i].dialogs[j].functionName);
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    GUILayout.Label("Content");
                    level.eventList[i].dialogs[j].content = GUILayout.TextArea(level.eventList[i].dialogs[j].content);
                    GUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();


                }
                GUILayout.BeginHorizontal();
                GUILayout.Space(5);
                if (GUILayout.Button("Add Enemy", GUILayout.ExpandWidth(false)))
                {
                    level.eventList[i].addEnemy();
                }
                GUILayout.EndHorizontal();


                for (int j = 0; j < level.eventList[i].enemies.Count; j++)
                {
                    Rect rect = EditorGUILayout.BeginVertical();
                    rect.x += 10;
                    GUI.Box(rect, GUIContent.none);
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    GUILayout.Label("Enemy " + (j + 1) + " :");
                    if (GUILayout.Button("Delete Enemy", GUILayout.ExpandWidth(false)))
                    {
                        level.eventList[i].deleteEnemy(j);
                        continue;
                    }
                    GUILayout.EndHorizontal();
                        
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    level.eventList[i].enemies[j].enemy = (GameObject)EditorGUILayout.ObjectField("Enemy type", level.eventList[i].enemies[j].enemy, typeof(GameObject), false);
                    GUILayout.Label("Prefab of the enemy.");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    level.eventList[i].enemies[j].spawnpoint = EditorGUILayout.IntField("Enemy spawnpoint", level.eventList[i].enemies[j].spawnpoint);
                    GUILayout.Label("0 : random, 1 : up, 2 : mid, 3 : bottom.");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    level.eventList[i].enemies[j].function = EditorGUILayout.TextField("Enemy function", level.eventList[i].enemies[j].function);
                    GUILayout.Label("Set the function of this enemy.");
                    GUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();


                }
                EditorGUILayout.EndVertical();


            }
            GUILayout.EndScrollView();
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(level);
        }

    }

    void CreateNewLevel()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...

        level = CreateLevel.Create();
    }

    void OpenLevel()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Level", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            level = AssetDatabase.LoadAssetAtPath(relPath, typeof(Level)) as Level;
        }
    }
}
#endif

