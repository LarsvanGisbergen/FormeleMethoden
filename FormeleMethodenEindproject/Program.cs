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

            //regex used: (a|b)(b|a)
            Regex a = new Regex('a');
            Regex b = new Regex('b');

            Regex astar = a.star();
            Regex bstar = b.star();
            Regex ab = a.dot(b);
            Regex ba = b.dot(a);
            Regex aorb = a.or(b);
            Regex abba = ab.dot(ba);
            Regex aborba = ab.or(ba);
            Regex abbaorba = abba.or(ba);
            Regex astarorbstar = astar.or(bstar);

            Regex final = ((a.star()).dot(aorb)).or(astar);
            //final = a.or(astar);
            //final = a.or(a.plus());
            final = a.or(bstar).dot(astar.or(aborba)).plus();
            //final = a.or(aborba);

            RegexToNFAConverter rnc = new RegexToNFAConverter();
            DFAbuilder db = rnc.RegexToNFA(final);

            //NFAToDFAConverter nfac = new NFAToDFAConverter("ab", db);
            //nfac.NFAToDFA();

            //db.printTransitionStructure();
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
