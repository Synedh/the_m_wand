using UnityEngine;
using System.Collections;
using Assets.Scripts.Fonctions;
using UnityEngine.UI;
using Assets;

public class EnnemiScript : MonoBehaviour {


    public float speed;
    public float attackSpeed;
    public float lastAttack;
    private int attackMode = 0;
    private Character chara;
    Node n;
    Text t;
	GameObject TEXDrawObject;
	TEXDraw TEXDrawComponent;

    // Use this for initialization
    void Start () {
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        n = Assets.Scripts.Fonctions.Tree.getRandomNodeForEnnemy();
		TEXDrawComponent = this.GetComponentInChildren<TEXDraw>();
        updateText();
    }
    private void updateText()
    {
		TEXDrawComponent.text = n.value;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackMode = 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (attackMode == 0)
            // Déplace l'entité vers le joueur
            this.transform.position = Vector2.MoveTowards(this.transform.position, chara.transform.position, Time.deltaTime * speed);
        else
        {
            lastAttack += Time.deltaTime;
            if (lastAttack > attackSpeed)
            {
                chara.getHit();
                lastAttack = 0;
            }
        }
	}
    void OnMouseDown()
    {
        // this object was clicked - do something
        //Debug.Log("click on ennemy");
        //FireballScript fireball = spellManager.Instance.currentSpellParticle.GetComponent<FireballScript>();
        //fireball.launchOnEnnemy(this.gameObject);

        if (spellManager.Instance.currentSpell != null)
        {
            Node newNode = Assets.Scripts.Fonctions.Tree.tryExecuteFunction(n.valueSimplified, spellManager.Instance.currentSpell);
            spellManager.Instance.removeSpell();
            if (newNode != null)
            {
                // Bonne fonction 
				if (newNode.value.Equals("x")) {
					newNode.value = "killed";
					Destroy(this.gameObject);
				}
                
                n = newNode;
                updateText();
            }
            else
            {
                // Mauvaise fonction
            }
        }
    }
}
