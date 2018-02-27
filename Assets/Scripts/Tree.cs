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
            { "racine","({0})^2" },
            { "square","\\root[2]{{{0}}}" },
			{ "inverse","\\frac{{1}}{{{0}}}" }


        };


        static List<Node> listNodeForRandom = new List<Node>();
        public static void createTree()
        {
            Node n = new Node();
            n.value = "x";
            n.children = new List<Node>();
            root = n;
            n.operatorToParent = "";
            recursiveCreateTree(n, 1);


        }
        private static void recursiveCreateTree(Node parent, int depth)
        {

            foreach (KeyValuePair<String, String> operatorDisplay in dict)
            {
                
                //si cette fonction annule la fonction d'avant, ne pas sauvegarder cette equation
                if (parent.operatorToParent.Equals("racine") && operatorDisplay.Key.Equals("square") || parent.operatorToParent.Equals("square") && operatorDisplay.Key.Equals("racine"))
                    continue;
                else if (parent.operatorToParent.Equals("inverse") && operatorDisplay.Key.Equals("inverse"))
                    continue;

                Node n3 = new Node();
                n3.value = String.Format(operatorDisplay.Value, parent.value);
                n3.operatorToParent = operatorDisplay.Key;
                n3.parent = parent;

                //la racine de l'inverse = l'inverse de la racine
                if (parent.operatorToParent.Equals("inverse") && operatorDisplay.Key.Equals("square"))
                {
                    String racine = String.Format(dict["square"], parent.parent.value);
                    n3.valueSimplified = String.Format(dict["inverse"], racine);
                    //Debug.Log("Simplified racine(1/x))"+n3.valueSimplified);
                }
                //la racine du carré = l'inverse du carré
                else if (parent.operatorToParent.Equals("inverse") && operatorDisplay.Key.Equals("racine"))
                {
                    String square = String.Format(dict["racine"], parent.parent.value);
                    n3.valueSimplified = String.Format(dict["inverse"], square);
                    //Debug.Log("Simplified square(1/x))" + n3.valueSimplified);
                }
                else
                    n3.valueSimplified = n3.value;



               
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

            //Debug.Log("Value root="+root.value);

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
            //Debug.Log("Value child: " + n.value + " Value simplified: " + n.valueSimplified  + " Operator to parent-> " + n.operatorToParent);

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

        public static Node tryExecuteFunction (String value, String function)
        {
            foreach(Node n in listNodeForRandom)
            {
                if (n.valueSimplified.Equals(value))
                {
                    if(n.operatorToParent.Equals(function))
                    {
                        return n.parent;
                    }
                }
            }
            return null;
        }
    }
}
