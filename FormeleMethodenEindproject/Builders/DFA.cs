using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethodenEindproject
{
    public class DFA
    {

        private char[] alphabet;
        private List<Node> nodes;
        private List<Transition> transitions;

        public DFA(char[] alphabet, List<Node> nodes, List<Transition> transitions)
        {
            this.alphabet = alphabet;
            this.nodes = nodes;
            this.transitions = transitions;
        }

        public char[] Alphabet { get => alphabet; }
        internal List<Node> Nodes { get => nodes; }
        internal List<Transition> Transitions { get => transitions; }

        /// <summary>
        /// Compares the DFA object to a given DFA object based on its transitions.
        /// Will return true if transitions match.
        /// </summary>
        public bool isIdentical(DFA dfa)
        {
            if (transitions.Count != dfa.transitions.Count)
            {
                return false;
            }
            foreach (Transition t in this.transitions)
            {
                if (!dfa.transitions.Contains(t))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// prints useful information about the contents of the DFA.
        /// contents consist of: alphabet string, amount of nodes and amount of transitions.
        /// </summary>
        public override string ToString() {
            string tmp_a = new string(this.alphabet);
            return string.Format("Alphabet: {0}\nNodes amount: {1}\nTransitions amount: {2}\n", tmp_a, this.nodes.Count, this.transitions.Count);
        }
    }


}
