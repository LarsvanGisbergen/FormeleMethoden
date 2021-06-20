using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethodenEindproject.Converters
{
    /// <summary>
    /// Kan grammatica generen op basis van een gegeven DFA.
    /// </summary>
    class DFAToGrammarConverter
    {
        
        public static List<Tuple<string, string>> DFAToGrammar(DFA dfa)
        {
            // List<Tuple<String, List<String>>> bevat node namen met een bijbehorende lijst met transities die naar andere node namen wijzen.
            // Bijv:
            // >1 -> a2, b3
            //  2 -> a1 
            // *3 -> b1

            List<Tuple<string, string>> grammar = new List<Tuple<string, string>>();
            List<Transition> transitions = dfa.Transitions;

            
            foreach (Transition t in transitions)
            {
                grammar.Add(new Tuple<string, string>(t.Origin.ToString(), t.Symbol.ToString() + t.Dest.ToString()));
            }
                
            return grammar;
        }

        

        public static void printGrammar(List<Tuple<string,string>> grammar)
        {
            Console.WriteLine("Grammar:");
            foreach(Tuple<string,string> t in grammar)
            {

                Console.WriteLine(t.Item1 + " -> " + t.Item2);
            }
        }
    }
}
