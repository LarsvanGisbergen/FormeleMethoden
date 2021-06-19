using FormeleMethodenEindproject.RegularExpression;
using System;
using System.Collections.Generic;
using FormeleMethodenEindproject.Converters;
using FormeleMethodenEindproject.Graphviz;
using System.Threading.Tasks;
using System.IO;

namespace FormeleMethodenEindproject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //DFAbuilder builder = new DFAbuilder("ab");

            //builder.addNode(true, false, 0);
            //builder.addNode(false, false, 1);
            //builder.addNode(false, true, 2);



            ////example regex: ab*a
            //builder.addTransition(0, 1, 'a');
            //builder.addTransition(1, 1, 'b');
            //builder.addTransition(1, 2, 'a');

            //DFA dfa = builder.createDFA();


            //Console.WriteLine(dfa);

            //regex used: ab+ba
            Regex a = new Regex('a');
            Regex b = new Regex('b');
            Regex ab = a.dot(b);
            Regex ba = b.dot(a);
            Regex abplusba = (ab.plus()).dot(ba);

            Regex complex = abplusba;
            Regex final = complex;

            RegexToNFAConverter rnc = new RegexToNFAConverter();
            DFAbuilder db = rnc.RegexToNFA(final);
            //db.printTransitionStructure();
            Console.WriteLine("Word accepted: " + db.acceptWord("ababbba"));
            //SortedSet<string> language = RegexLogic.regexToLanguage(final, 5);
            //foreach (string word in language)
            //{
            //    Console.WriteLine(word);
            //}
            //Console.WriteLine("Language size: " + language.Count);

            Graphbuilder g = new Graphbuilder(db.createDFA());
            await g.createGraph();

        }
    }
}
