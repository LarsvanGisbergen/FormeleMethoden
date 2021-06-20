using FormeleMethodenEindproject.RegularExpression;
using System;
using System.Collections.Generic;
using FormeleMethodenEindproject.Converters;
using FormeleMethodenEindproject.Graphviz;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace FormeleMethodenEindproject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Regex a = new Regex('a');
            Regex b = new Regex('b');
            Regex ab = a.dot(b);
            Regex ba = b.dot(a);

            Regex final = a.dot(b);

            //Regex to NFA
            RegexToNFAConverter rnc = new RegexToNFAConverter("abe");
            DFAbuilder db = rnc.RegexToNFA(final);

            //Generate DFAbuilders that start, end, or contain their paramater Regex
            string alphabet = "abe"; // 
            DFAbuilder startswitha = new DFAbuilder(alphabet).createDFAStartsWith(new Regex('a'));
            DFAbuilder endswitha = new DFAbuilder(alphabet).createDFAEndsWith(new Regex('a'));
            DFAbuilder containsa = new DFAbuilder(alphabet).createDFAContains(new Regex('a'));

            //NFA to DFA
            NFAToDFAConverter nfac = new NFAToDFAConverter();
            DFAbuilder nfa = nfac.NFAToDFA(db);

            //Generate graph
            Graphbuilder g = new Graphbuilder(db, "test");
            await g.createGraph();

            //Are these two dfa identical?
            Regex aorb = a.or(b);
            DFAbuilder firstbuilder = rnc.RegexToNFA(aorb);
            NFAToDFAConverter nfacfirst = new NFAToDFAConverter();
            DFAbuilder nfa1 = nfacfirst.NFAToDFA(firstbuilder);

            Graphbuilder g1 = new Graphbuilder(nfa1,"nfa1");
            await g1.createGraph();

            Regex bora = b.or(a);
            DFAbuilder secondbuilder = rnc.RegexToNFA(aorb);
            NFAToDFAConverter nfacsecond = new NFAToDFAConverter();
            DFAbuilder nfa2 = nfacsecond.NFAToDFA(firstbuilder);

            Graphbuilder g2 = new Graphbuilder(nfa2, "nfa2");
            await g2.createGraph();

            if ((nfa1.createDFA()).isIdentical(nfa2.createDFA()))
            {
                Console.WriteLine("Identical");
            }
            else 
            {
                Console.WriteLine("Not identical");
            }


            ////generate whole language from alphabet
            //Console.WriteLine("All possible words:");
            //IEnumerable<string> words = RegexLogic.generateFullLanguage("ab", 5);
            //printStrings(words);


            ////generate all valid words from alphabet and regex
            //Console.WriteLine("All valid words:");
            //IEnumerable<string> validWords = RegexLogic.regexToLanguage(final, 8);
            //printStrings(validWords);

            ////generate all nonvalid words from alphabet and regex
            //IEnumerable<string> nonValidWords = RegexLogic.generateNonValidWords(final, "ab", 5);
            //Console.WriteLine("All non-valid words:");
            //printStrings(nonValidWords);
        }

        private static void printStrings(IEnumerable<string> strings)
        {
            foreach (var item in strings)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Amount of words: " + strings.Count() + "\n");
        }
    }
}


