using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets;

public class spellManager : MonoBehaviour
{

    private Image FirstSpell;
    private Image SecondSpell;
    private Image ThirdSpell;
    private Image FourthSpell;
    private Image FifthSpell;

    public Sprite EmptySprite;
    public Sprite LinearPosSprite;
    public Sprite LinearNegSprite;
    public Sprite SquareSprite;
    public Sprite RootSprite;
    public Sprite ExponentialSprite;
    public Sprite LogSprite;
    public Sprite IntegralSprite;
    public Sprite InverseSprite;
    public Sprite DeriveSprite;

    public Image currentSpellObject;
    private Vector3 defaultSpellPosition;
    public string currentSpellName;
    public FireballScript currentSpellParticle;

    public static spellManager Instance;
    public bool isDragged;
    private GameObject spwanParticlePoint;

    private Animator charaAnimator;

    // Use this for initialization
    void Start()
    {
        spwanParticlePoint = GameObject.Find("SpawnParticlePoint");
        FirstSpell = GameObject.FindGameObjectWithTag("FirstSpell").GetComponent<Image>();
        SecondSpell = GameObject.FindGameObjectWithTag("SecondSpell").GetComponent<Image>();
        ThirdSpell = GameObject.FindGameObjectWithTag("ThirdSpell").GetComponent<Image>();
        FourthSpell = GameObject.FindGameObjectWithTag("FourthSpell").GetComponent<Image>();
        FifthSpell = GameObject.FindGameObjectWithTag("FifthSpell").GetComponent<Image>();

        charaAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        Instance = this;
        currentSpellName = null;
        isDragged = false;
    }

    void addSpellSprite(Sprite spellSprite)
    {
        if (FirstSpell.sprite == EmptySprite)
            FirstSpell.sprite = spellSprite;
        else if (SecondSpell.sprite == EmptySprite)
            SecondSpell.sprite = spellSprite;
        else if (ThirdSpell.sprite == EmptySprite)
            ThirdSpell.sprite = spellSprite;
        else if (FourthSpell.sprite == EmptySprite)
            FourthSpell.sprite = spellSprite;
        else if (FifthSpell.sprite == EmptySprite)
            FifthSpell.sprite = spellSprite;
    }

    public void addSpell(string spellName)
    {
        switch (spellName)
        {
            case "lineairePositive":
                addSpellSprite(LinearPosSprite);
                break;
            case "lineaireNegative":
                addSpellSprite(LinearNegSprite);
                break;
            case "square":
                addSpellSprite(SquareSprite);
                break;
            case "racine":
                addSpellSprite(RootSprite);
                break;
            case "exponentielle":
                addSpellSprite(ExponentialSprite);
                break;
            case "logarithme":
                addSpellSprite(LogSprite);
                break;
            case "integrale":
                addSpellSprite(IntegralSprite);
                break;
            case "inverse":
                addSpellSprite(InverseSprite);
                break;
            case "derivee":
                addSpellSprite(DeriveSprite);
                break;
        }
    }

    public void resetSpell()
    {
        currentSpellObject.color = new Color(1, 1, 1);
        currentSpellObject = null;
        currentSpellName = null;
    }

    public void removeSpell()
    {
        currentSpellObject.sprite = EmptySprite;
        resetSpell();
    }

    public void onSpellClick(Image spell)
    {

        // Put all spells to white        
        FirstSpell.color = new Color(1, 1, 1);
        SecondSpell.color = new Color(1, 1, 1);
        ThirdSpell.color = new Color(1, 1, 1);
        FourthSpell.color = new Color(1, 1, 1);
        FifthSpell.color = new Color(1, 1, 1);


        if (spell == currentSpellObject)
        {
            resetSpell();
            return;
        }
		//currentSpellParticle = ((GameObject)GameObject.Instantiate(Resources.Load("Ressources/Prefabs/Particles/Thunder"), spwanParticlePoint.transform.position, Quaternion.identity)).GetComponent<FireballScript>();

        if (spell.sprite.Equals(EmptySprite))
        {
            return;
        }
        else if (spell.sprite.Equals(LinearPosSprite))
        {
            currentSpellName = "lineairePositive";
        }
        else if (spell.sprite.Equals(LinearNegSprite))
        {
            currentSpellName = "lineaireNegative";
        }
        else if (spell.sprite.Equals(SquareSprite))
        {
			//currentSpellParticle = ((GameObject)GameObject.Instantiate(Resources.Load("Ressources/Prefabs/Particles/Fireball"), spwanParticlePoint.transform.position, Quaternion.identity)).GetComponent<FireballScript>();
            currentSpellName = "square";
        }
        else if (spell.sprite.Equals(RootSprite))
        {
			//currentSpellParticle = ((GameObject)GameObject.Instantiate(Resources.Load("Ressources/Prefabs/Particles/Thunder"), spwanParticlePoint.transform.position, Quaternion.identity)).GetComponent<FireballScript>();
            currentSpellName = "racine";
        }
        else if (spell.sprite.Equals(ExponentialSprite))
        {
            currentSpellName = "exponentielle";
        }
        else if (spell.sprite.Equals(LogSprite))
        {
            currentSpellName = "logarithme";
        }
        else if (spell.sprite.Equals(IntegralSprite))
        {
            currentSpellName = "integrale";
        }
        else if (spell.sprite.Equals(InverseSprite))
        {
            currentSpellName = "inverse";
        }
        else if (spell.sprite.Equals(DeriveSprite))
        {
            currentSpellName = "derivee";
        }

        charaAnimator.SetBool("spell_charge", true);

        // Set current spell
        currentSpellObject = spell;
        currentSpellObject.color = new Color(0, 1, 1);
    }

    public void onSpellDrag(Image spell)
    {
        if (spell.sprite != EmptySprite)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100.0f);
            spell.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }

    public void onStartDrag(Image spell)
    {
        isDragged = true;
        defaultSpellPosition = spell.transform.position;
    }

    public void onEndDrag(Image spell)
    {
        isDragged = false;
        Debug.Log(Vector2.Distance(defaultSpellPosition, spell.transform.position));
        if (Vector2.Distance(defaultSpellPosition, spell.transform.position) > 1.5f)
        {
            currentSpellObject = spell;
            removeSpell();
        }
        spell.transform.position = defaultSpellPosition;
    }
}