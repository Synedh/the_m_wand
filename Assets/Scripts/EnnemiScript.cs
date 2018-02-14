using UnityEngine;
using System.Collections;

public class EnnemiScript : MonoBehaviour {


    public float speed;
    public float attackSpeed;
    public float lastAttack;
    private int attackMode = 0;
    private Character chara;


    // Use this for initialization
    void Start () {
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
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
            this.transform.Translate(Vector2.left * Time.deltaTime * speed);
        else
        {
            lastAttack += Time.deltaTime;
            if (lastAttack > attackSpeed)
            {
                chara.CurrentLife -= 1;
                lastAttack = 0;
            }
            
        }
	
	}
}
