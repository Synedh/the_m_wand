using UnityEngine;
using System.Collections;
using Assets.Scripts.Fonctions;
using UnityEngine.UI;
using Assets;

public class EnnemiScript : MonoBehaviour {


    public float speed;
    public float attackSpeed;
    public float lastAttack;
    public string function;
    public int pushBack;
    public GameObject lightning;
    
    bool doAttack;
    Character chara;
    Animator enemy_animator;
    Animator player_animator;

    public Node currentNode;
	TEXDraw TEXDrawComponent;

    void Start () {
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        currentNode = Assets.Scripts.Fonctions.Tree.getNodeFromString(function.Split(','));
        enemy_animator = GetComponent<Animator>();
        player_animator = chara.GetComponent<Animator>();
		doAttack = false;

        TEXDrawComponent = this.GetComponentInChildren<TEXDraw>();
        updateText(currentNode.value);
    }

    public static EnnemiScript Create(string function, Transform spawnPoint)
    {
        GameObject newObject = Instantiate(Resources.Load("Prefabs/Entities/Enemy"), spawnPoint.position, spawnPoint.rotation) as GameObject;
        EnnemiScript ennemi = newObject.GetComponent<EnnemiScript>();
        ennemi.function = function;
        return ennemi;
    }

    void updateText(string str)
    {
		TEXDrawComponent.text = str;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
	if (collision.gameObject.tag == "Player") {
		doAttack = true;
		Vibration.Vibrate(250);		
		

	}

    }

    void ThrowLightningBolt(GameObject endObject)
    {
        this.GetComponentInChildren<DigitalRuby.LightningBolt.LightningBoltScript>().EndObject = endObject;
        this.GetComponentInChildren<DigitalRuby.LightningBolt.LightningBoltScript>().Trigger();
    }

    void ThrowLightningBoltChara()
    {
        this.GetComponentInChildren<DigitalRuby.LightningBolt.LightningBoltScript>().EndObject = null;
        this.GetComponentInChildren<DigitalRuby.LightningBolt.LightningBoltScript>().EndPosition = chara.transform.position + new Vector3(54, 33)/100;
        //chara.GetComponentInChildren<ParticleSystem>().Play();
        this.GetComponentInChildren<DigitalRuby.LightningBolt.LightningBoltScript>().Trigger();
    }

    void Update () {
        if (!doAttack)
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
            if (newNode != null) // Bonne fonction appliquée
            {
                int score = 5;
                int multip = 1;
                doAttack = false;
                updateText(newNode.value);
                player_animator.SetBool("spell_cast", true);
                ThrowLightningBoltChara();
                enemy_animator.SetBool("getHit", true);
                Sound.loadSound("Sounds/spell");

                GetComponent<Rigidbody2D>().AddForce(Vector2.right * pushBack, ForceMode2D.Impulse);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i] != this.gameObject)
                    {
                        if (currentNode == enemies[i].GetComponent<EnnemiScript>().currentNode)
                        {
                            enemies[i].GetComponent<EnnemiScript>().GetComboHit(this.gameObject, newNode);
                            multip++;
                        }
                    }
                }
                if (newNode.value.Equals("x")) // Ennemi mort
                {
                    score += 5;
                    Sound.loadSound("Sounds/death_enemy");
                    enemy_animator.SetBool("die", true);
                    speed = 0;
                    GetComponent<BoxCollider2D>().isTrigger = true;
                }
                ScoreManager.instance.addScore(score * multip);
                currentNode = newNode;
            }
            else // Mauvaise fonction appliquée
            {
                Sound.loadSound("Sounds/wrong_spell");
                Shake.sendShake(0.5f, 0.07f);
                // Retour utilisateur de mauvais spell appliqué
            }
            spellManager.Instance.removeSpell();
        }
    }



    public int GetComboHit(GameObject source, Node newNode)
    {
        int score = 5;
        ThrowLightningBolt(source);
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * pushBack, ForceMode2D.Impulse);
        enemy_animator.SetBool("getHit", true);
        doAttack = false;
        if (newNode.value.Equals("x")) // Ennemi mort
        {
            score = 10;
            enemy_animator.SetBool("die", true);
            speed = 0;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        updateText(newNode.value);
        currentNode = newNode;
        return score;
    }
}
