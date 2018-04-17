using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
	static AudioSource audioSource;

	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	public static void sendSound(string soundName)
	{
		AudioClip clip = (AudioClip)Resources.Load (soundName);
		audioSource.Stop();
		audioSource.PlayOneShot (clip);
	}

    public static void stopSound()
    {
        audioSource.Stop();
    }
}
