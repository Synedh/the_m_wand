using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {
    Transform cameraPosition;
    Vector3 defaultCameraPosition;
    static float startDuration;
    static float duration;
    static float power;
    static bool doShake;

	void Start () {
        cameraPosition = this.gameObject.transform;
        defaultCameraPosition = this.gameObject.transform.localPosition;
        doShake = false;
	}
	
	void Update () {
		if (doShake)
        {
            cameraPosition.localPosition = defaultCameraPosition + Random.insideUnitSphere * power;

            if (startDuration < duration)
                startDuration += Time.deltaTime;
            else
            {
                cameraPosition.localPosition = defaultCameraPosition;
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
