using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethodenEindproject
{
    public class DFAbuilder
    {
        private char[] alphabet;
        private string alphabet_as_string; // in case of extra functionality
        private List<Node> nodes; 
        private List<Transition> transitions;
        private int lastNodeID;


        public int LastNodeID { get => lastNodeID; }

        /// <summary>
        /// DFAbuilder requires an alphabet to which node interactions will be checked against
        /// </summary>
        /// <param name="alphabet"></param>
        public DFAbuilder(string alphabet)
        {
            this.alphabet_as_string = alphabet;
            this.alphabet = alphabet.ToCharArray();
            this.nodes = new List<Node>();
            this.transitions = new List<Transition>();
            this.lastNodeID = -1; //-1 means it has not yet been initialized, will be set to >0 when addNode() is called.
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
            this.lastNodeID++;
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

        public DFA createBasicDFA()
        {
            this.lastNodeID = 1;
            return new DFA(this.alphabet, new List<Node>() { new Node(true, false, 0), new Node(false,true,1) }, new List<Transition>() {new Transition(0,1,'-') });
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

        public void printTransitionStructure()
        {
            Console.WriteLine("transitions:");
            foreach (Transition t in this.transitions)
            {
                Console.WriteLine("from: " + t.Origin + " to: " + t.Dest + " terminal: " + t.Symbol);
            }
            Console.WriteLine("");

            Console.WriteLine("nodes:");
            foreach (Node n in this.nodes)
            {
                Console.WriteLine("begin: " + n.Begin + " end: " + n.End + " id: " + n.Id);
            }
        }

        
    }
}
