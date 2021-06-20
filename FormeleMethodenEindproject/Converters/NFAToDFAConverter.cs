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

        public NFAToDFAConverter(DFAbuilder nfa)
        {
            this._dfabuilder = new DFAbuilder(nfa.getAlphabetAsString()); // e = epsilon
            this.nfa = nfa;
        }

        public DFAbuilder NFAToDFA() {
            //Generate original transition table
            List<List<string>> ortable = generateOriginalTable();

            //Print original table
            //printTable(ortable);

            char[] alphabet = nfa.getAlphabet();

            //Init new table
            List<List<string>> table = new List<List<string>>();


            //Init first row with first row of original table
            table.Add(ortable[0].Select(x => {
                if (x.Equals("")) { x = "{}"; }
                return x;
            }).ToList());

            //Index of table
            int index = 0;
            bool done = false;

            while (!done)
            {
                for (int i = 1; i < ortable[0].Count; i++)
                {
                    string subject = table[index][i];
                    if (subject.Equals("")) subject = "{}";
                    if (subject.Equals("{}"))
                    {
                        if (!existInTable(table, subject))
                        {
                            List<string> temp = new List<string>();
                            for (int j = 0; j < alphabet.Length; j++)
                            {
                                temp.Add("{}");
                            }
                            table.Add(temp);
                        }
                    }
                    else if (existInTable(table, subject)) { }
                    else if (existInTable(ortable, subject))
                    {
                        //Console.WriteLine(subject + " exists");
                        //Console.WriteLine("Add new row to table with node " + subject);
                        table.Add(ortable[index]);
                    }
                    else
                    {
                        //Console.WriteLine(subject + " doesnt exist");
                        table.Add(generateRowFromOriginalTable(ortable, subject));
                    }
                }
                index++;
                if (index > table.Count -1) { 
                    done = true; 
                }
            }
            //Print completed table
            //printTable(table);
            DFAbuilder builder = new DFAbuilder(nfa.getAlphabetAsString());
            List<string> beginNodes = nfa.getBeginNodes().Select(x => "" + x.Id).ToList();
            List<string> endNodes = nfa.getEndNodes().Select(x => "" + x.Id).ToList();

            int nodeId = 0;
            //Generate new Nodes
            table.ForEach(list =>
            {
                builder.addNode(isBeginOrEndNode(list[0],beginNodes), isBeginOrEndNode(list[0], endNodes), nodeId, list[0]);
                nodeId++;
            });

            bool beginNodeIsEndNode = false;
            recursiveNodeDest(builder.getNodes()[0], 'a', true);

            endNodes.ForEach(x => {
                if (recList.Contains(int.Parse(x))) {
                    beginNodeIsEndNode = true;
                    Console.WriteLine("Is end node");
                }
            });

            if (beginNodeIsEndNode) { builder.setEndNode(0); }

            //Generate new Transitions
            table.ForEach(list =>
            {
                for (int i = 0; i < alphabet.Length -1; i++)
                {
                    builder.addTransition(getIndexFromTable(table,list[0]), getIndexFromTable(table, list[i+ 1]),alphabet[i]);
                }
            });
            return builder;
        }

        private int getIndexFromTable(List<List<string>> table, string id) {
            int index = -1;
            for (int i = 0; i < table.Count; i++)
            {
                if (table[i][0].Equals(id)) { index = i; }
            }
            return index;
        }

        private bool isBeginOrEndNode(string id, List<string> nodes)
        {
            List<string> ids = id.Split(',').ToList();
            bool isBeginOrEnd = false;
            nodes.ForEach(x =>
            {
                ids.ForEach(y =>
                {
                    if (x.Equals(y)) { isBeginOrEnd = true; }
                });
            });
            return isBeginOrEnd;
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

                for (int j = 0; j < alphabet.Length - 1; j++)
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
            foreach (int value in recList.OrderByDescending(x => x).Reverse()) { values += value + ","; }
            if (values.Length > 0) { values = values.Remove(values.Length - 1, 1); }
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

        public bool existInTable(List<List<string>> table, string id) {
            if (table.Count == 0) { return false; }
            List<string> temp = new List<string>();
            table.ForEach(list => temp.Add(list[0]));
            return temp.Contains(id);
        }

        public List<string> generateRowFromOriginalTable(List<List<string>> ortable, string id) {
            //Console.WriteLine("this is id: " + id);
            List<String> temp = new List<string>();
            for (int i = 0; i < nfa.getAlphabet().Length; i++)
            {
                temp.Add("");
            }
            temp[0] = id;
            List<string> ids = id.Split(',').ToList();
            //ids.ForEach(x => Console.WriteLine(x));

            //Loop through ortable
            for (int i = 0; i < ortable.Count; i++) // 0 - 12
            {
                //Loop through ids
                ids.ForEach(id => // 3 4
                {
                    //Check if they match
                    if (id.Equals(ortable[i][0])) { // 3 4 equals 0 - 12
                        //Loop through ortable and add values
                        for (int j = 1; j < nfa.getAlphabet().Length; j++) 
                        {
                            temp[j] += ortable[i][j] + ",";
                        }
                    }
                });
            }

            //Fixup and reformat row
            temp = reformatRow(temp);
            //Console.WriteLine("row:");
            //temp.ForEach(x => Console.WriteLine(x));

            return temp;
        }

        public List<string> reformatRow(List<string> input) {
            List<string> output = new List<string>();
            input.ForEach(item =>
            {
                List<string> ids = item.Split(',').ToList();
                ids = ids.Distinct().ToList();
                ids = ids.FindAll(x => !x.Equals(""));
                string values = "";
                foreach (string value in ids) { values += value + ","; }
                if (values.Length > 0) { values = values.Remove(values.Length - 1, 1); }
                if (values.Equals(""))
                {
                    output.Add("{}");
                }
                else
                {
                    output.Add(values);
                }
            });
            return output;
        }
        //public int findIndexFrom


    }
}
