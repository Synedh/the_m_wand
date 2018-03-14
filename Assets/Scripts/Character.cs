using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {
    
    public int CurrentLife = 5;
    public int maxLife = 5;
    public Heart heart;
    public Heart[] hearts;
    public GameObject lifebar;

    private void Start()
    {
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

        if (CurrentLife > 1)
        {
            hearts[CurrentLife - 1].shatter();
            CurrentLife--;
        }
        else
        {
            // A FAIRE PROPRE
            LoadSceneOnClick.titleText = "SCORE : " + ScoreManager.instance.scoreString;
            LoadSceneOnClick.startButtonText = "RESTART";
            SceneManager.LoadScene(0);
        }
    }

    private void Update()
    {
    }
}
