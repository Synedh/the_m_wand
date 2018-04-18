using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Fonctions
{
    class Tree: MonoBehaviour
    {
        //le nombre de fonctions maximal pour arriver a x
        public int maxDepth;
        static Node root;
        //cette liste sert à pouvoir choisir un noeud au hazard car dans un arbre c'est plus compliqué
        static List<Node> listNodeForRandom;

        //ce dictionnaire regroupe toutes les fonctions, il sert à générer l'arbre récursivement. En clé la fonction qui annule, en clé l'affichage (TexDraw)
        //{0} est remplacé par l'affichage du noeud parent. 
        static Dictionary<String, String> dict = new Dictionary<String, String>(){
			{ "racine", "({0})^2" }, // carrée et sa fonction annulle est racine carrée
			{ "inverse", "\\frac{{1}}{{{0}}}" }, // inverse et sa fonction est inverse 
			{ "square", "\\root[2]{{{0}}}" }, // racine carrée et sa fonction annulle est carrée
			{ "exponentielle", "log({0})" },// log et sa fonction annulle est exp
			{ "logarithme", "\\e^{{{0}}}" }, // exp et sa fonction annulle est log
			{ "integrale", "\\frac{{\\partial{{x}}}}{{\\partial{{x}}}}({0})" }, // derivée et sa fonction annulle est intégrale
			{ "derivee", "\\int{{{0}}}" } // intégrale et sa fonction annulle est derivée 
        };

        void Start()
        {
            listNodeForRandom = new List<Node>();
        }
        void createTree()
        {
            //On cree le noeud racine x
            Node n = new Node();
            n.value = "x";
            n.depth = 0;
            n.children = new List<Node>();
            root = n;
            n.operatorToParent = "";

            //On cree l'arbre 
            recursiveCreateTree(n, 1);
			
        }

        private void recursiveCreateTree(Node parent, int depth)
        {
            //Pour chaque fonction du dictionnaire
            foreach (KeyValuePair<String, String> operatorDisplay in dict)
            {                
                //si cette fonction annule la fonction d'avant, ne pas sauvegarder cette equation
                if (parent.operatorToParent.Equals("racine") && operatorDisplay.Key.Equals("square") || parent.operatorToParent.Equals("square") && operatorDisplay.Key.Equals("racine"))
                    continue;
                if (parent.operatorToParent.Equals("inverse") && operatorDisplay.Key.Equals("inverse"))
                    continue;
				if (parent.operatorToParent.Equals("logarithme") && operatorDisplay.Key.Equals("exponentielle") || parent.operatorToParent.Equals("exponentielle") && operatorDisplay.Key.Equals("logarithme"))
					continue;
				
				if (parent.operatorToParent.Equals("derivee") && operatorDisplay.Key.Equals("integrale") || parent.operatorToParent.Equals("integrale") && operatorDisplay.Key.Equals("derivee"))
					continue;


                Node n3 = new Node();
                //la valeur du noeud est l'affichage de la fonction qui entoure la valeur du noeud parent (String format remplace {0} par le premier parametre)
                //si la fonction est carré --> ({0})^2, et le parent est 1/x. value = (1/x)^2
                n3.value = String.Format(operatorDisplay.Value, parent.value);
                n3.depth = depth;
                n3.operatorToParent = operatorDisplay.Key; 
                n3.parent = parent;

                //la racine de l'inverse = l'inverse de la racine
                if (parent.operatorToParent.Equals("inverse") && operatorDisplay.Key.Equals("square"))
                {
                    String racine = String.Format(dict["square"], parent.parent.value);
                    //la valeur simplifié est donc la meme, ce qui permet de pouvoir utiliser deux fonctions, soit inverse soit square (voir tryExecuteFunction)
                    n3.valueSimplified = String.Format(dict["inverse"], racine);
                }
                //la racine du carré = l'inverse du carré
                else if (parent.operatorToParent.Equals("inverse") && operatorDisplay.Key.Equals("racine"))
                {
                    String square = String.Format(dict["racine"], parent.parent.value);
                    //la valeur simplifié est donc la meme, ce qui permet de pouvoir utiliser deux fonctions, soit inverse soit racine (voir tryExecuteFunction)
                    n3.valueSimplified = String.Format(dict["inverse"], square);
                }
                else
                    //sinon la valeur simplifié est simplement la valeur
                    n3.valueSimplified = n3.value;
               
                parent.children.Add(n3);
                listNodeForRandom.Add(n3);
                // si on est pas encore arrivé à la profondeur max on continue la création de l'arbre
                if (depth < maxDepth)
                {
                    n3.children = new List<Node>();
                    recursiveCreateTree(n3, depth + 1);
                }
            }
        }
        //ces sont seulement utilisés pour tester, elles affichent tous les noeuds créés
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


        // permet d'obtenir tous les noeuds à une certaines profondeur
        private static List<Node> getNodesOfDepth(int depth)
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < listNodeForRandom.Count; ++i)
                if (listNodeForRandom[i].depth == depth)
                    nodes.Add(listNodeForRandom[i]);
            return nodes;
        }

        // permet d'obtenir un noeud au hazard à une certaine profondeur
        public static Node getRandomNodeOfDepth(int depth)
        {
            List<Node> nodesOfDepth = getNodesOfDepth(depth);
            return nodesOfDepth[new System.Random().Next() % nodesOfDepth.Count()];
        }

        public static Node getNodeFromString(String[] str)
        {
            Node result = root;
            foreach (String s in str)
                foreach (Node next in result.children)
                {
                    if (next.operatorToParent.CompareTo(s) == 0)
                    {
                        result = next;
                        break;
                    }
                }
            print(result.value);
            
            return result;
        }

        public static Dictionary<String, String> getDict()
        {
            return dict;
        }

        //permet de savoir si la fonction utilisé simplifie le noeud, si oui retourne le coeud parent, sinon return null
        public static Node tryExecuteFunction (String value, String function)
        {
            // on parcourt tous les noeuds de l'arbre à la recherche d'un noeud qui matche la valeur et la fonction de simplification
            foreach(Node n in listNodeForRandom)
            {
                
                // on utilise la valeur simplifié du noeud pour les cas ou deux fonctions sont possibles pour un meme affichage
                if (n.valueSimplified.Equals(value) && n.operatorToParent.Equals(function))
                {
                    return n.parent;
                }
            }
            return null;
        }
    }
}
