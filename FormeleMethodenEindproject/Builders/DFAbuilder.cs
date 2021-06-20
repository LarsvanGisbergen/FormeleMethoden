using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FormeleMethodenEindproject
{
    public class DFAbuilder
    {
        private char[] alphabet;
        private string alphabet_as_string; // in case of extra functionality
        private List<Node> nodes; 
        private List<Transition> transitions;
        private int lastNodeID;
        private bool accepted = false;


        public int LastNodeID { get => lastNodeID; }

        /// <summary>
        /// DFAbuilder requires an alphabet to which node interactions will be checked against
        /// </summary>
        /// <param name="alphabet"></param>
        /// The alphabet describes the characters that the language is allowed to use for its words
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

        public bool addNode(bool begin, bool end, int id, string name)
        {
            foreach (Node node in nodes)
            {
                if (node.Id == id)
                {
                    return false;
                }
            }
            nodes.Add(new Node(begin, end, id, name));
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
            bool exists = false;
            foreach (Node node in nodes)
            {
                if (node.Id == origin) { found_or = true; };
                if (node.Id == dest) { found_dest = true; };
            }
            foreach (Transition transition in transitions) {
                if (transition.Origin == origin && transition.Dest == dest && transition.Symbol == symbol) { exists = true; };            
            }
            if (found_or && found_dest && !exists) {
                transitions.Add(new Transition(origin, dest, symbol));
                return true;
            }
            return false;
        }

        public void deleteTransition(int origin, int dest, char symbol) {
            this.transitions.RemoveAll(x => (x.Origin == origin && x.Dest == dest && x.Symbol.Equals(symbol)));
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

        /// <summary>
        /// acceptWord() recursively checks paths in the (n)dfa to see if any path leads to an endnode which indicates the word is part of the language described by the (n)dfa.
        /// If a single path leads to an endnode the word is contained in the language. The global accepted boolean is updated if the word belongs to the language ending the recursive loop.
        /// acceptRecursive() used 3 variables to perform a check: curChar keeps track of the current character index in the word.
        /// word is the given string of characters that describe the word that is being checked.
        /// transition holds data from all pathways from a node to other nodes that need to be checked.
        /// </summary>       
        public bool acceptWord(string word)
        {
            // check to see if any chars are not in alphabet
            foreach(char c in word)
            {
                if (!isInAlphabet(c))
                {
                    return false;
                }
            }

            //recursively check paths until endnode is reached, accepted will be set to true if valid path is found
            char[] word_chars = word.ToCharArray();
            List<Transition> tmp = getTransitionsFromOrigin(0);
            foreach (Transition t in getTransitionsFromOrigin(0))
            {               
                acceptRecursive(0, word_chars, t);               
            }
            return accepted;         
        }

        private void acceptRecursive(int curChar, char[] word, Transition transition)
        {                     
            if (transition.Symbol == 'e')               
                {
                if (getNodeFromId(transition.Dest).End)
                {                                       
                    accepted = true;
                }               
                foreach (Transition t in getTransitionsFromOrigin(transition.Dest))
                    {                      
                        acceptRecursive(curChar, word, t);
                    }
                }

            

            else if (curChar < word.Length && transition.Symbol == word[curChar])
                {                
                foreach (Transition t in getTransitionsFromOrigin(transition.Dest))
                    {
                        acceptRecursive(curChar + 1, word, t);
                    }
            }

            else if(curChar == word.Length - 1)
            {
                if (getNodeFromId(transition.Dest).End)
                {                                      
                    accepted = true;
                }
                else 
                {
                    foreach (Transition t in getTransitionsFromOrigin(transition.Dest))
                    {
                        if (t.Symbol.Equals("e")) {
                            acceptRecursive(curChar, word, t);
                        }
                        
                    }
                }             
            }
                      
        }
        public int getTransitionsSize()
        {
            return this.transitions.Count;
        }

        /// <summary>
        /// This method returns all the transitions from the nfa that have the same origin as the parameter
        /// </summary>
        /// <param name="origin"></param> This is the origin paramater to find the list items
        /// <returns></returns> List of transitions
        public List<Transition> getTransitionsFromOrigin(int origin)
        {
            return transitions.FindAll(t =>
            {
                return t.Origin == origin;
            });
        }

        public List<Node> getNodes() {
            return nodes;
        }

        public List<Transition> getTransitions() {
            return transitions;
        }

        public Node getNodeFromId(int id) {
            return nodes.Find(n =>
            {
                return n.Id == id;
            });
        }

        public char[] getAlphabet() {
            return alphabet;
        }
        public string getAlphabetAsString()
        {
            return alphabet_as_string;
        }
        public override string ToString()
        {
            string builderString = "Alphabet: " + this.alphabet + "\nNodes: " + this.nodes + "\nTransitions: " + this.transitions;
            return builderString;
        }

        private bool isInAlphabet(char c)
        {
            foreach(char chr in alphabet)
            {
                if (chr.Equals(c))
                {
                    return true;
                }
            }
            return false;
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
                Console.WriteLine("begin: " + n.Begin + " end: " + n.End + " id: " + n.Id + " name: " + n.Name);
            }
        }
        public List<Node> getBeginNodes()
        {
            return nodes.Where(x => x.Begin).ToList();
        }
        public List<Node> getEndNodes() {
            return nodes.Where(x => x.End).ToList();
        }
        
    }
}
