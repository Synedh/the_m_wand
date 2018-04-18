using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource audioSource;
    string SoundName;
    bool hasPlayed;

	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
        hasPlayed = false;
	}

    void Update()
    {
        if (!audioSource.isPlaying && hasPlayed)
        {
            audioSource.Stop();
            DestroyImmediate(this.gameObject);
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Resources.Load(SoundName) as AudioClip);
            hasPlayed = true;
        }
    }

    public void stop()
    {
        if (audioSource)
        {
            audioSource.Stop();
            DestroyImmediate(this.gameObject);
        }
    }

    public static Sound loadSound(string soundName)
    {
        GameObject newSound = Instantiate(Resources.Load("Prefabs/Sound"), new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;

        Sound soundScript = newSound.GetComponent<Sound>();
        soundScript.SoundName = soundName;
        return soundScript;
    }
}
