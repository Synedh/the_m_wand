using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
	static AudioSource audioSource;

	void Start () {
		audioSource = gameObject.AddComponent <AudioSource>() as AudioSource;
	}

	public static void sendSound(string soundName)
	{
		AudioClip clip = (AudioClip)Resources.Load (soundName);
		if (audioSource.isPlaying)
			audioSource.Stop ();
		audioSource.PlayOneShot (clip);
	}
}
