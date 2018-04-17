﻿using UnityEngine;
using System.Collections;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class CreateLevel {
    
	public static Level Create () {
        Level level = ScriptableObject.CreateInstance<Level>();
        AssetDatabase.CreateAsset(level, "Assets/Levels/newLevel.asset");
        AssetDatabase.SaveAssets();
        return level;
    }
}
#endif
