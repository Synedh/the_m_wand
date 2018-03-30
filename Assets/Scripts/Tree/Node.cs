using System;
using System.Collections.Generic;

namespace Assets.Scripts.Fonctions
{
    public class Node
    {
        public String value;
        public int depth;
        public String valueSimplified;
        public String operatorToParent;
        public Node parent;
        public List<Node> children;
    }
}
