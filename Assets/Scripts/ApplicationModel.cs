using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationModel {

    static public int level = 0;
    static public int score = 0;
    static public bool isWinned = false;
    static public bool gameIsPaused = false;
    static public Level currentLevel;

    public static void clear()
    {
        level = 0;
        score = 0;
        isWinned = false;
        gameIsPaused = false;
        currentLevel = null;
    }
}
