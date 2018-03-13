using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spellManager : MonoBehaviour {

    private Image FirstSpell;
    private Image SecondSpell;
    private Image ThirdSpell;

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

    public string currentSpell;
    public Image currentSpellObject;

    public static spellManager Instance;
    private Flash flash;
    public GameObject fireBallObject;
    private GameObject spwanPoint;
    public GameObject currentSpellParticle;
    // Use this for initialization
    void Start () {
        spwanPoint = GameObject.Find("SpawnParticlePoint");
        FirstSpell = GameObject.FindGameObjectWithTag("FirstSpell").GetComponent<Image>();
        SecondSpell = GameObject.FindGameObjectWithTag("SecondSpell").GetComponent<Image>();
        ThirdSpell = GameObject.FindGameObjectWithTag("ThirdSpell").GetComponent<Image>();

        Instance = this;
        currentSpell = null;
    }

    void addSpellSprite(Sprite spellSprite) {
        if (FirstSpell.sprite == EmptySprite)
            FirstSpell.sprite = spellSprite;
        else if (SecondSpell.sprite == EmptySprite)
            SecondSpell.sprite = spellSprite;
        else if (ThirdSpell.sprite == EmptySprite)
            ThirdSpell.sprite = spellSprite;
    }

    public void addSpell(string spellName) {
        switch (spellName) {
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
        currentSpell = null;

    }

    public void removeSpell() {
        currentSpellObject.sprite = EmptySprite;
        resetSpell();
    }

    public void onSpellClick(Image spell) {
        // flash.Instance.flash = true;
        //spell.sprite = EmptySprite;
        //var NewGameObject = GameObject.Instantiate(fireBallObject);

        // Put all spells to white        
        FirstSpell.color = new Color(1, 1, 1);
        SecondSpell.color = new Color(1, 1, 1);
        ThirdSpell.color = new Color(1, 1, 1);


        if (spell == currentSpellObject) {
            resetSpell();
            return;
        }
        else if (spell.sprite.Equals(EmptySprite)) {
            return;
        }
        else if (spell.sprite.Equals(LinearPosSprite)) {
            currentSpell = "lineairePositive";
        }
        else if (spell.sprite.Equals(LinearNegSprite)) {
            currentSpell = "lineaireNegative";
        }
        else if (spell.sprite.Equals(SquareSprite)) {
            currentSpellParticle = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Particles/Fireball"), spwanPoint.transform.position, Quaternion.identity);
            currentSpell = "square";
        }
        else if (spell.sprite.Equals(RootSprite)) {
            currentSpellParticle = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Particles/Thunder"), spwanPoint.transform.position, Quaternion.identity);
            currentSpell = "racine";
        }
        else if (spell.sprite.Equals(ExponentialSprite)) {
            currentSpell = "exponentielle";
        }
        else if (spell.sprite.Equals(LogSprite)) {
            currentSpell = "logarithme";
        }
        else if (spell.sprite.Equals(IntegralSprite)) {
            currentSpell = "integrale";
        }
        else if (spell.sprite.Equals(InverseSprite)) {
            currentSpell = "inverse";
        }
        else if (spell.sprite.Equals(DeriveSprite)) {
            currentSpell = "derivee";
        }

        // Set current spell
        currentSpellObject = spell;
        currentSpellObject.color = new Color(0, 1, 1);
    }
}
