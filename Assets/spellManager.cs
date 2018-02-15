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

    public static spellManager Instance;

    private Flash flash;

    // Use this for initialization
    void Start () {
        FirstSpell = GameObject.FindGameObjectWithTag("FirstSpell").GetComponent<Image>();
        SecondSpell = GameObject.FindGameObjectWithTag("SecondSpell").GetComponent<Image>();
        ThirdSpell = GameObject.FindGameObjectWithTag("ThirdSpell").GetComponent<Image>();

        Instance = this;
    }

    void addSpellSprite(Sprite spellSprite) {
        print(spellSprite);
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
            default:
                break;
        }
    }

    public void onSpellClick(Image spell){
        // flash.Instance.flash = true;
        spell.sprite = EmptySprite;
    }
}
