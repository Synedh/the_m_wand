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
			{ "racine", "({0})^2" }, // carrée et sa fonction annulle est racine carrée
			{ "inverse", "\\frac{{1}}{{{0}}}" }, // inverse et sa fonction est inverse 
			{ "square", "\\root[2]{{{0}}}" }, // racine carrée et sa fonction annulle est carrée
			{ "exponentielle", "log({0})" },// log et sa fonction annulle est exp
			{ "logarithme", "\\e^{{{0}}}" }, // exp et sa fonction annulle est log
			{ "integrale", "\\frac{{\\partial{{0}}}}{{\\partial{{x}}}}({{x}})" }, // derivée et sa fonction annulle est intégrale
			{ "derivee", "\\int{{x}}" } // intégrale et sa fonction annulle est derivée 
        };


        static List<Node> listNodeForRandom = new List<Node>();
        public static void createTree()
        {
            Node n = new Node();
            n.value = "x";
            n.depth = 0;
            n.children = new List<Node>();
            root = n;
            n.operatorToParent = "";
            recursiveCreateTree(n, 1);
			// displayTree();
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
				if (parent.operatorToParent.Equals("logarithme") && operatorDisplay.Key.Equals("exponentielle") || parent.operatorToParent.Equals("exponentielle") && operatorDisplay.Key.Equals("logarithme"))
					continue;
				
				if (parent.operatorToParent.Equals("derivee") && operatorDisplay.Key.Equals("integrale") || parent.operatorToParent.Equals("integrale") && operatorDisplay.Key.Equals("derivee"))
					continue;
                Node n3 = new Node();
                n3.value = String.Format(operatorDisplay.Value, parent.value);
                n3.depth = depth;
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
                if (depth < 2) /* CHIFFRE A MODIFIER POUR LIMITE DE PROFONDEUR DE GENERATION */
                {
                    n3.children = new List<Node>();
                    recursiveCreateTree(n3, depth + 1);
                }
            }
        }
        public static void displayTree()
        {
            recursiveDisplayTree(root);
        }

        private static void recursiveDisplayTree(Node n)
        {
            if (n.children != null)
            {
                foreach (Node child in n.children)
                {
                    Debug.Log(child.value);
                    recursiveDisplayTree(child);
                }
            }
        }

        private static List<Node> getNodesOfDepth(int depth)
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < listNodeForRandom.Count; ++i)
                if (listNodeForRandom[i].depth == depth)
                    nodes.Add(listNodeForRandom[i]);
            return nodes;
        }

        public static void setLevel(int level)
        {
            if (level > 0)
                dict = dict.Skip(0).Take(level).ToDictionary(x => x.Key, x => x.Value);
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

        public static Node getRandomNodeOfDepth(int depth)
        {
            List<Node> nodesOfDepth = getNodesOfDepth(depth);
            return nodesOfDepth[new System.Random().Next() % nodesOfDepth.Count()];
        }

        public static Node tryExecuteFunction (String value, String function)
        {
            foreach(Node n in listNodeForRandom)
            {
                if (n.valueSimplified.Equals(value))
                {
                    return n.parent;
                }
            }
            return null;
        }
    }
}
