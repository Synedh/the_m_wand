using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Fonctions
{
    class Node
    {
        public String value;
        public int depth;
        public String valueSimplified;
        public String operatorToParent;
        public Node parent;
        public List<Node> children;

        
    }
}
