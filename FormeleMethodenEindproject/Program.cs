using System;

namespace FormeleMethodenEindproject
{
    class Program
    {
        static void Main(string[] args)
        {
            DFAbuilder builder = new DFAbuilder("ab");

            builder.addNode(true, false, 0);
            builder.addNode(false, false, 1);
            builder.addNode(false, true, 2);



            //example regex: ab*a
            builder.addTransition(0, 1, 'a');
            builder.addTransition(1, 1, 'b');
            builder.addTransition(1, 2, 'a');

            DFA dfa = builder.createDFA();

            Console.WriteLine(dfa);
        }
    }
}
