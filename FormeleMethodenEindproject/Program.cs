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
            NFAToDFAConverter nfac = new NFAToDFAConverter(db);
            DFAbuilder nfa = nfac.NFAToDFA();

            //Generate graph
            Graphbuilder g = new Graphbuilder(db);
            await g.createGraph();

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


