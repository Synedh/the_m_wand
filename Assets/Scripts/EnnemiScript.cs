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
    public int difficulty;
    private Character chara;
    private float timer;
    private bool getHit = false;
    public int pushBack;
    Animator animator;
    Animator chara_animator;


    Node n;
    Text t;
	GameObject TEXDrawObject;
	TEXDraw TEXDrawComponent;

    // Use this for initialization
    void Start () {
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        n = Assets.Scripts.Fonctions.Tree.getRandomNodeOfDepth(difficulty);
        timer = 0;
        animator = GetComponent<Animator>();
        chara_animator = chara.GetComponent<Animator>();

        TEXDrawComponent = this.GetComponentInChildren<TEXDraw>();
        updateText();
    }

    public static EnnemiScript Create(int difficulty, Transform spawnPoint)
    {
        GameObject newObject = Instantiate(Resources.Load("Prefabs/Characters/Ennemi"), spawnPoint.position, spawnPoint.rotation) as GameObject;
        EnnemiScript ennemi = newObject.GetComponent<EnnemiScript>();
        ennemi.difficulty = difficulty;
        return ennemi;
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
    void Update () {
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

        if (getHit)
        {
           
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * pushBack, ForceMode2D.Impulse);
            getHit = false;
        }
        if (n.value.Equals("x"))
        {
            animator.SetBool("die", true);
            speed = 0;
            attackMode = 0;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    public void stopStagger()
    {
        animator.SetBool("getHit", false);
    }


    void OnMouseDown()
    {
        //FireballScript fireball = spellManager.Instance.currentSpellParticle.GetComponent<FireballScript>();
        //fireball.launchOnEnnemy(this.gameObject);
        if (spellManager.Instance.currentSpellName != null)
        {
            Node newNode = Assets.Scripts.Fonctions.Tree.tryExecuteFunction(n.valueSimplified, spellManager.Instance.currentSpellName);
            spellManager.Instance.removeSpell();
            if (newNode != null) // Bonne fonction appliquée
            {
                ScoreManager.instance.addScore(1);
				if (newNode.value.Equals("x"))
                    ScoreManager.instance.addScore(1);
                n = newNode;
                chara_animator.SetBool("spell_cast", true);
                animator.SetBool("getHit", true);
                getHit = true;
                updateText();
            }
            else // Mauvaise fonction appliquée
            {
                // Retour utilisateur de mauvais spell appliqué
            }
        }
    }
}
