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
            //regex used: ab+ba
            Regex a = new Regex('a');
            Regex b = new Regex('b');
            Regex ab = a.dot(b);
            Regex ba = b.dot(a);
            Regex aborba = ab.or(ba);
            Regex abba = ab.dot(ba);
            Regex complex = (abba.star()).or(aborba);

            Regex final = complex;

            //Regex to NFA
            RegexToNFAConverter rnc = new RegexToNFAConverter();
            DFAbuilder db = rnc.RegexToNFA(final);

            //NFA to DFA
            NFAToDFAConverter nfac = new NFAToDFAConverter("ab", db);
            DFAbuilder nfa = nfac.NFAToDFA();

            //Generate graph
            Graphbuilder g = new Graphbuilder(nfa);
            await g.createGraph();


            ////generate whole language from alphabet
            //Console.WriteLine("All possible words:");
            //IEnumerable<string> words = RegexLogic.generateFullLanguage("ab", 5);
            //printStrings(words);


            ////generate all valid words from alphabet and regex
            //Console.WriteLine("All valid words:");
            //IEnumerable<string> validWords = RegexLogic.regexToLanguage(final, 5);
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


