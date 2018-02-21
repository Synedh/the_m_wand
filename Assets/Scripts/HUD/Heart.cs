using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour {

    public Sprite heart_full;
    public Sprite heart_empty;
    public int currentState = 1;
    private int shakeVal = -1;
    private Vector2 savedPos;

    //TODO function shatter and tremble

	// Use this for initialization
	void Start () {

		
	}

    public void setState(int i)
    {
        currentState = i;
    }

    public void shake()
    {
        savedPos = transform.position;
        shakeVal = 10;
        currentState = 2;
    }

    public void shatter()
    {
        currentState = 0;
    }

    // Update is called once per frame
    void Update () {
        switch (currentState){
            case 0: this.GetComponent<Image>().sprite = heart_empty;break;
            case 1: this.GetComponent<Image>().sprite = heart_full; break;
            case 2:
                transform.position = savedPos;
                transform.position = savedPos + Random.insideUnitCircle * shakeVal * Time.deltaTime;
                shakeVal--;
                break;
        }
        if (shakeVal == 0)
        {
            shakeVal = -1;
            currentState = 1;
            transform.position = savedPos;
        }
		
	}
}
