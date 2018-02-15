using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Fonctions
{
    class Tree
    {
        static Node root;
        static Dictionary<String, String> dict = new Dictionary<String, String>(){
            { "racine","{0}^2" },
            { "square","root[2]{{{0}}}" },
            { "inverse","1/{0}" }


        };


        static List<Node> listNodeForRandom = new List<Node>();
        public static void createTree()
        {
            Node n = new Node();
            n.value = "x";n.children = new List<Node>();
            root = n;
            n.operatorToParent = "";
            recursiveCreateTree(n, 1);


        }
        private static void recursiveCreateTree(Node parent, int depth)
        {

            foreach (KeyValuePair<String, String> operatorDisplay in dict)
            {
                
                if (parent.operatorToParent.Equals("racine") && operatorDisplay.Key.Equals("square") || parent.operatorToParent.Equals("square") && operatorDisplay.Key.Equals("racine"))
                    continue;
                if (parent.operatorToParent.Equals("inverse") && operatorDisplay.Key.Equals("inverse"))
                    continue;
                Node n3 = new Node();
                n3.value = String.Format(operatorDisplay.Value, parent.value);
                n3.operatorToParent = operatorDisplay.Key;
                n3.parent = parent;
                parent.children.Add(n3);
                listNodeForRandom.Add(n3);
                if (depth < 2)
                {
                    n3.children = new List<Node>();
                    recursiveCreateTree(n3, depth + 1);
                }

            }



        }
        public static void displayTree()
        {

            Debug.Log("Value root="+root.value);

            recursiveDisplayTree(root);
        }
        private static void recursiveDisplayTree(Node n)
        {
            if (n.children != null)
            {
                foreach (Node child in n.children)
                {

                    recursiveDisplayTree(child);
                    //
                }
            }
            Debug.Log("Value child: " + n.value + " Operator to parent-> " + n.operatorToParent);

        }

        public static Node getRandomNodeForEnnemy()
        {
            if (listNodeForRandom.Count > 0)
            {
                System.Random r = new System.Random();
                int index = r.Next() % listNodeForRandom.Count();
                return listNodeForRandom[index];
            }
            return null;
        }
    }
}
