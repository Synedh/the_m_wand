using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Fonctions
{
    public class Function : MonoBehaviour
    {
        private Node node;
        // Use this for initialization
        void Start()
        {
            Tree.createTree();
            node = Tree.getRandomNodeForEnnemy();
        }

        // Update is called once per frame
        void Update()
        {
            print(node.value);
        }
    }
}
