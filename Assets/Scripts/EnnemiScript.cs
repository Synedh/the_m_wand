using UnityEngine;
using System.Collections;
using Assets.Scripts.Fonctions;
using UnityEngine.UI;
using Assets;

public class EnnemiScript : MonoBehaviour {


    public float speed;
    public float attackSpeed;
    public float lastAttack;
    public int difficulty;
    private int attackMode = 0;
    private Character chara;
    public int pushBack;
    Animator enemy_animator;
    Animator player_animator;

    Node currentNode;
	TEXDraw TEXDrawComponent;
    public GameObject lightning;

    // Use this for initialization
    void Start () {
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        currentNode = Assets.Scripts.Fonctions.Tree.getRandomNodeOfDepth(difficulty);
        enemy_animator = GetComponent<Animator>();
        player_animator = chara.GetComponent<Animator>();

        TEXDrawComponent = this.GetComponentInChildren<TEXDraw>();
        updateText(currentNode.value);
    }

    public static EnnemiScript Create(int difficulty, Transform spawnPoint)
    {
        GameObject newObject = Instantiate(Resources.Load("Prefabs/Characters/Ennemi"), spawnPoint.position, spawnPoint.rotation) as GameObject;
        EnnemiScript ennemi = newObject.GetComponent<EnnemiScript>();
        ennemi.difficulty = difficulty;
        return ennemi;
    }

    private void updateText(string str)
    {
		TEXDrawComponent.text = str;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            attackMode = 1;
    }

    void ThrowLightningBolt(GameObject endObject)
    {
        chara.GetComponentInChildren<DigitalRuby.LightningBolt.LightningBoltScript>().EndObject = endObject;
        chara.GetComponentInChildren<DigitalRuby.LightningBolt.LightningBoltScript>().Trigger();
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * pushBack, ForceMode2D.Impulse);
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

    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    public void stopStagger()
    {
        enemy_animator.SetBool("getHit", false);
    }


    void OnMouseDown()
    {
        if (spellManager.Instance.currentSpellName != null)
        {
            Node newNode = Assets.Scripts.Fonctions.Tree.tryExecuteFunction(currentNode.valueSimplified, spellManager.Instance.currentSpellName);
            spellManager.Instance.removeSpell();

            if (newNode != null) // Bonne fonction appliquée
            {
                ScoreManager.instance.addScore(1);
                updateText(newNode.value);
                player_animator.SetBool("spell_cast", true);
                ThrowLightningBolt(this.gameObject);
                enemy_animator.SetBool("getHit", true);
                attackMode = 0;

                if (newNode.value.Equals("x")) // Ennemi mort
                {
                    ScoreManager.instance.addScore(1);
                    enemy_animator.SetBool("die", true);
                    speed = 0;
                    attackMode = 0;
                    GetComponent<BoxCollider2D>().isTrigger = true;
                }
                currentNode = newNode;
            }
            else // Mauvaise fonction appliquée
            {
                // Retour utilisateur de mauvais spell appliqué
            }
        }
    }
}
