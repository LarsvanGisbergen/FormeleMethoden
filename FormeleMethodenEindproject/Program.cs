﻿using FormeleMethodenEindproject.RegularExpression;
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
        static public void Main(string[] args)
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
            Regex aborba = ab.or(ba);

            Regex final = aborba;

            //Regex to NFA
            RegexToNFAConverter rnc = new RegexToNFAConverter();
            DFAbuilder db = rnc.RegexToNFA(final);
            //db.printTransitionStructure();
            Console.WriteLine("Word accepted: " + db.acceptWord("abba"));

            //NFA to DFA
            //NFAToDFAConverter nfac = new NFAToDFAConverter("ab", db);
            //nfac.NFAToDFA();

            //db.printTransitionStructure();
            //SortedSet<string> language = RegexLogic.regexToLanguage(final, 5);
            //foreach (string word in language)
            //{
            //    Console.WriteLine(word);
            //}
            //Console.WriteLine("Language size: " + language.Count);

            //Generate graph
            //Graphbuilder g = new Graphbuilder(db.createDFA());
            //await g.createGraph();


            //generate whole language from alphabet
            Console.WriteLine("All possible words:");
            IEnumerable<string> words = RegexLogic.generateFullLanguage("ab", 5);
            printStrings(words);
            

            //generate all valid words from alphabet and regex
            Console.WriteLine("All valid words:");
            IEnumerable<string> validWords = RegexLogic.regexToLanguage(final, 5);
            printStrings(validWords);
            

            //generate all nonvalid words from alphabet and regex
            IEnumerable<string> nonValidWords = RegexLogic.generateNonValidWords(final, "ab", 5);
            Console.WriteLine("All non-valid words:");
            printStrings(nonValidWords);
            
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


