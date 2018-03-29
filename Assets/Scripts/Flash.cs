using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour {
    Image flash;
    static float startDuration;
    static Color color;
    static float duration;
    static bool doFlash;

	void Start () {
		flash = this.gameObject.GetComponent<Image>();
        doFlash = false;
	}

    void Update()
    {
        if (doFlash)
        {
            flash.color = Color.Lerp(color, new Color(0, 0, 0, 0), startDuration);

            if (startDuration < duration)
                startDuration += Time.deltaTime;
            else
                doFlash = false;
        }
    }

    public static void sendFlash (Color flashColor, float flashDuration) {
        color = flashColor;
        duration = flashDuration;
        startDuration = 0f;
        doFlash = true;
    }
}
