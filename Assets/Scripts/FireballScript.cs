using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class FireballScript : MonoBehaviour
    
    {
        public float speed = 2;
        GameObject target;
        public void launchOnEnnemy(GameObject ennemy)
        {
            target = ennemy;

        }
        // Update is called once per frame
        void Update()
        {
            if (target != null)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, Time.deltaTime * speed);
            }
        }
        void OnParticleCollision(GameObject other)
        {
            if (!other.name.Equals("Character"))
                Debug.Log(other);
            if (GameObject.ReferenceEquals(other, target))
            {
                Debug.Log("Target reached");
                var burst = transform.Find("Burst");
                burst.gameObject.SetActive(true);
                GameObject currentBurst = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Particles/Burst"), this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                Destroy(target);
               
               
               

                
            }
            

        }
       

    }
    
}
