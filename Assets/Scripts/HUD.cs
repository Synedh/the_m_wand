using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HUD : MonoBehaviour {

    public Sprite[] HearstSprite;

    public Image HeartUI;

    private Character chara;


    private void Start()
    {
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }   

    private void Update()
    {
        HeartUI.sprite = HearstSprite[chara.CurrentLife];
    }



}
