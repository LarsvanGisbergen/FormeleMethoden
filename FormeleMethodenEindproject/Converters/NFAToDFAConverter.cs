using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FormeleMethodenEindproject.Converters
{
    class NFAToDFAConverter
    {
        public DFAbuilder _dfabuilder;
        public DFAbuilder nfa;
        private HashSet<int> recList = new HashSet<int>();

        public NFAToDFAConverter(string alphabet, DFAbuilder nfa)
        {
            this._dfabuilder = new DFAbuilder(alphabet); // e = epsilon
            this.nfa = nfa;
        }

        public void NFAToDFA() {

            List<List<string>> ortable = generateOriginalTable();

            printTable(ortable);

            Console.WriteLine("NFA TO DFA CONVERSION");
            char[] alphabet = nfa.getAlphabet();
            List<List<string>> table = new List<List<string>>();
            int i = 0;

            //Iterate through nodes

            //Startnode
            Node node = nfa.getNodes()[0];

            //Init new row
            table.Add(new List<string>());

            //Init columns
            Console.WriteLine("Initializing colums for i:" + i);
            for (int j = 0; j < alphabet.Length; j++)
            {
                table[i].Add("");
            }

            //Set First column value to node id
            table[i][0] = "" + node.Id;

            //Iterate through alphabet
            for (int j = 0; j < alphabet.Length - 1; j++)
            {
                //Get all destination nodes recursively
                recursiveNodeDest(node, alphabet[j], false); // b
                //Add them in the appropriate column
                table[i][j + 1] =  getRecListAsString();
            }
        }

        public void recursiveNodeDest(Node n, char c, bool used) {
            List<Transition> transitions = nfa.getTransitionsFromOrigin(n.Id);

            transitions.ForEach(t =>
            {
                if (t.Symbol.Equals('e'))
                {
                    recursiveNodeDest(nfa.getNodeFromId(t.Dest), c, used);
                    if (used) {
                        recList.Add(t.Dest);
                    }
                }
                else if (t.Symbol.Equals(c))
                {
                    if (!used)
                    {
                        recList.Add(t.Dest);
                        used = true;
                        recursiveNodeDest(nfa.getNodeFromId(t.Dest), c, used);
                    }
                }
            });
        }

        public List<List<string>> generateOriginalTable() {
            List<List<string>> table = new List<List<string>>();
            List<Node> nodes = nfa.getNodes();
            char[] alphabet = nfa.getAlphabet();

            for (int i = 0; i < nodes.Count; i++)
            {
                table.Add(new List<string>());
                table[i].Add("" + nodes[i].Id);

                for (int j = 0; j < alphabet.Length; j++)
                {
                    recursiveNodeDest(nodes[i], alphabet[j], false); // b
                    table[i].Add(getRecListAsString());
                    recList = new HashSet<int>();
                }
            }
            return table;
        }

        public string getRecListAsString() {
            string values = "";
            foreach (int value in recList.OrderByDescending(x => x).Reverse()) { values += value + " "; }
            return values;
        }

        public void printTable(List<List<string>> table) {
            Console.WriteLine("Table:");

            table.ForEach(x =>
            {
                string temp = "";
                x.ForEach(y =>
                {
                    temp += y + " | ";
                });
                Console.WriteLine(temp);
            });
        }


    }
}
