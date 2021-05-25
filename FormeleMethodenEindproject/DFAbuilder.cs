using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethodenEindproject
{
    class DFAbuilder
    {
        private char[] alphabet;
        private List<Node> nodes; 
        private List<Transition> transitions;
        

        
        public DFAbuilder(string alphabet)
        {
            this.alphabet = alphabet.ToCharArray();
            this.nodes = new List<Node>();
            this.transitions = new List<Transition>();
        }

        public bool addNode(bool begin, bool end, int id)
        {
            foreach (Node node in nodes)
            {
                if(node.Id == id)
                {
                    return false;
                }
            }
            nodes.Add(new Node(begin,end,id));
            return true;
        }

        public bool addTransition(int origin, int dest, char symbol)
        {
            if(!(Array.IndexOf(alphabet,symbol) > -1))
            {
                return false;
            }

            bool found_or = false;
            bool found_dest = false;
            foreach (Node node in nodes)
            {

                if (node.Id == origin) { found_or = true; };
                if (node.Id == dest) { found_dest = true; };
            }
            if (found_or && found_dest) {
                transitions.Add(new Transition(origin, dest, symbol));
                return true;
            }
            return false;
        }

        public DFA createDFA()
        {
            return new DFA(this.alphabet, this.nodes, this.transitions);
        }
        public int getTransitionsSize()
        {
            return this.transitions.Count;
        }
        public override string ToString()
        {
            string builderString = "Alphabet: " + this.alphabet + "\nNodes: " + this.nodes + "\nTransitions: " + this.transitions;
            return builderString;
        }
    }
}
