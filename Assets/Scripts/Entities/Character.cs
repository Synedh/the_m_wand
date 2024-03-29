﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    public int CurrentLife = 5;
    public int maxLife = 5;
    public Heart heart;
    public Heart[] hearts;
    public GameObject lifebar;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

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
    }

    public void getHit()
    {
        animator.SetBool("isHurt", true);
		Sound.loadSound("Sounds/ennemi_attack");
        Shake.sendShake(0.4f, 0.07f);

        for (int i = 0; i < CurrentLife; i++)
        {
            hearts[i].shake();
        }

        if (CurrentLife <= maxLife / 3 + 1)
            Flash.sendFlash(new Color(1, 0, 0, 0.3f), 0.9f);

        if (CurrentLife > 1)
        {
            hearts[CurrentLife - 1].shatter();
            CurrentLife--;
        }
        else
		{
            SceneManager.LoadScene(2);
        }
    }

    public void stopStagger()
    {
        animator.SetBool("isHurt", false);
    }

    public void stopCast()
    {
        animator.SetBool("spell_cast", false);
        animator.SetBool("spell_charge", false);

    }
}
