using FormeleMethodenEindproject.RegularExpression;
using System;
using System.Collections.Generic;
using FormeleMethodenEindproject.Converters;

namespace FormeleMethodenEindproject
{
    class Program
    {
        static void Main(string[] args)
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

            //regex used: abba

            Regex r1 = new Regex('a');
            Regex r2 = new Regex('b');
            Regex r3 = new Regex('b');
            Regex r4 = new Regex('a');
            Regex r5 = r1.dot(r2);
            Regex r6 = r5.dot(r3);
            Regex r7 = r6.dot(r4);

            RegexToNFAConverter rnc = new RegexToNFAConverter();
            DFAbuilder db = rnc.RegexToNFA(r7);
            db.printTransitionStructure();
            

            //SortedSet<string> language = RegexLogic.regexToLanguage(r4, 5);
            //foreach (string word in language)
            //{
            //    Console.WriteLine(word);
            //}
            //Console.WriteLine("Language size: " + language.Count);



        }
    }
}
