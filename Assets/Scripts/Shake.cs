using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {
    private Transform cameraPosition;
    private Transform defaultCameraPosition;
    private static float startDuration;
    private static float duration;
    private static float power;
    private static bool doShake;

	void Start () {
        cameraPosition = this.gameObject.transform;
        defaultCameraPosition = this.gameObject.transform;
        doShake = false;
	}
	
	void Update () {
		if (doShake)
        {
            cameraPosition.localPosition = defaultCameraPosition.localPosition + Random.insideUnitSphere * power;

            if (startDuration < duration)
                startDuration += Time.deltaTime;
            else
            {
                cameraPosition = defaultCameraPosition;
                doShake = false;
            }
        }
	}

    public static void sendShake(float shakeDuration, float shakepower)
    {
        duration = shakeDuration;
        power = shakepower;
        startDuration = 0;
        doShake = true;
    }
}
