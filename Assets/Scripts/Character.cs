using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    
    public int CurrentLife = 5;
    public int maxLife = 5;
    private Character chara;
    public Heart heart;
    public Heart[] hearts;
    public GameObject lifebar;

    private void Start()
    {
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        hearts = new Heart[maxLife];
        RectTransform rt = (RectTransform)heart.transform;
        float width = rt.rect.width;

        for (int i = 0; i < maxLife; i++)
        {
            Heart h = Instantiate(heart, new Vector2(0, 0), Quaternion.identity);
            h.transform.SetParent(lifebar.transform, false);
            h.transform.position += transform.right * i;
            hearts[i] = h;
            if (i >= CurrentLife)
                hearts[i].shatter();
        }
        //TODO fonction a call qui retire une vie
    }

    public void getHit()
    {
        for (int i = 0; i < CurrentLife; i++)
        {
            hearts[i].shake();
        }
        hearts[CurrentLife-1].shatter();
        CurrentLife--;
        if (CurrentLife == 0)
        {
            //GameOver
        }
    }

    private void Update()
    {
        //HeartUI.sprite = HearstSprite[chara.CurrentLife];
    }
}
