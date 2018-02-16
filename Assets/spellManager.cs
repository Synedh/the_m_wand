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

    public static spellManager Instance;

    private Flash flash;
    public string currentSpell = null;

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
<<<<<<< HEAD
			case "inverse":
			addSpellSprite(InverseSprite);
			break;
		case "derive":
			addSpellSprite(DeriveSprite);
			break;
=======
            case "inverse":
                addSpellSprite(IntegralSprite);
                break;
>>>>>>> e927c19b26b16f2148b65d34af03ba4e22f826c1
            default:
                break;
        }
    }

    public void onSpellClick(Image spell){
        // flash.Instance.flash = true;
        //spell.sprite = EmptySprite;
        if(spell.sprite.Equals(LinearPosSprite))
                currentSpell = "lineairePositive";

        else if (spell.sprite.Equals(LinearNegSprite))
            currentSpell = "lineaireNegative";

        else if (spell.sprite.Equals(SquareSprite))
            currentSpell = "square";

        else if (spell.sprite.Equals(RootSprite))
            currentSpell = "racine";

        else if (spell.sprite.Equals(ExponentialSprite))
            currentSpell = "exponentielle";

        else if (spell.sprite.Equals(LogSprite))
            currentSpell = "logarithme";

        else if (spell.sprite.Equals(IntegralSprite))
            currentSpell = "integrale";   

    }
}
