using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using FormeleMethodenEindproject.RegularExpression;
using FormeleMethodenEindproject.Converters;

namespace FormeleMethodenEindproject.Testing
{
    class Testapplication
    {
        public Testapplication()
        {
        }
        public void startTest() {
            Console.WriteLine("Welcome to our testing application");
            Console.WriteLine(getMenu());
            while (true) {
                handleKeyInput(Console.ReadKey().Key);
            }
        }

        public void handleKeyInput(ConsoleKey c) {
            switch (c) {
                case ConsoleKey.NumPad0:
                    methodCase0();
                    break;
                case ConsoleKey.NumPad1:
                    methodCase1();
                    break;
                case ConsoleKey.NumPad2:
                    methodCase2();
                    break;
                case ConsoleKey.NumPad3:
                    methodCase3();
                    break;
                case ConsoleKey.NumPad4:
                    methodCase4();
                    break;
                case ConsoleKey.NumPad5:
                    methodCase5();
                    break;
                default:
                    Console.WriteLine("\nKey not recognized");
                    break;
            }
            Console.WriteLine(getMenu());
        }

        private void methodCase0()
        {
            bool loop = true;
            while (loop) {
                Console.WriteLine("\n" +
                    "0 | Get example of hardcoded regex\n" +
                    "1 | Get example of hardcoded NDFA\n" +
                    "2 | Exit submenu\n");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.NumPad0:
                        openPicture("hardcodedregex.png");
                        break;
                    case ConsoleKey.NumPad1:
                        openPicture("hardcodedndfa.png");
                        break;
                    case ConsoleKey.NumPad2:
                        Console.WriteLine("\nExiting submenu\n");
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("\nKey not recognized");
                        break;
                }
            }
        }

        private void methodCase1()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("\n" +
                    "0 | Generate all possible words in language\n" +
                    "1 | Generate language for regex (ab|ba)*\n" +
                    "2 | Generate NOT language for regex (ab|ba)*\n" +
                    "3 | Exit submenu\n");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.NumPad0:
                        Console.WriteLine("\nEnter length between 1-9 to generate the full language of alphabet \"ab\"");
                        IEnumerable<string> words = RegexLogic.generateFullLanguage("ab", int.Parse(Console.ReadKey().KeyChar + ""));
                        printStrings(words);
                        break;
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("\nThe language for regex (ab|ba)* of length 6");
                        Regex aborba = new Regex('a').dot(new Regex('b')).or(new Regex('b').dot(new Regex('a')));
                        IEnumerable<string> validWords = RegexLogic.regexToLanguage(aborba.star(), 6);
                        printStrings(validWords);
                        break;
                    case ConsoleKey.NumPad2:
                        Regex aborbastar = (new Regex('a').dot(new Regex('b')).or(new Regex('b').dot(new Regex('a')))).star();
                        IEnumerable<string> nonValidWords = RegexLogic.generateNonValidWords(aborbastar, "ab", 6);
                        Console.WriteLine("\nThe NOT language for regex (ab|ba)* of length 6");
                        printStrings(nonValidWords);
                        break;
                    case ConsoleKey.NumPad3:
                        Console.WriteLine("\nExiting submenu\n");
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("\nKey not recognized");
                        break;
                }
            }
        }        
        
        private void methodCase2()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("\n------------------------------------------\n" +
                    "0 | Test the acceptor for ab\n" +
                    "1 | Open the (ab|ba)* diagram\n" +
                    "2 | Exit submenu\n");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.NumPad0:
                        Console.WriteLine("\nEnter testing string (type \"-\") to exit");
                        string word = Console.ReadLine();
                        if (!word.Equals("-"))
                        {
                            Regex ab = new Regex('a').dot(new Regex('b'));
                            if (new RegexToNFAConverter("abe").RegexToNFA(ab).acceptWord(word))
                            {
                                Console.WriteLine("\nSucces!");
                                Console.WriteLine("The word: " + word + " does get accepted by the (ab|ba)* regex");
                            }
                            else
                            {
                                Console.WriteLine("\nFailure");
                                Console.WriteLine("The word: " + word + " does not get accepted by the (ab|ba)* regex");
                            }
                        }
                        break;
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("\nOpening (ab|ba)* diagram\n");
                        openPicture("aborbastar.png");
                        break;
                    case ConsoleKey.NumPad2:
                        Console.WriteLine("\nExiting submenu\n");

                        loop = false;
                        break;
                    default:
                        Console.WriteLine("\nKey not recognized");
                        break;
                }
            }
        }

        private void methodCase3()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("\n------------------------------------------\n" +
                    "0 | Use specific constructors to create dfa starting with regex a (open the picture)\n" +
                    "1 | Use specific constructors to create dfa ending with regex a (open the picture)\n" +
                    "2 | Use specific constructors to create dfa containing regex a (open the picture)\n" +
                    "3 | Exit submenu");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.NumPad0:
                        Console.WriteLine("\nOpening picture");
                        openPicture("startswitha.png");
                        break;
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("\nOpening picture");
                        openPicture("endswitha.png");
                        break;
                    case ConsoleKey.NumPad2:
                        Console.WriteLine("\nOpening picture");
                        openPicture("containsa.png");
                        break;
                    case ConsoleKey.NumPad3:
                        Console.WriteLine("\nExiting submenu\n");
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("\nKey not recognized");
                        break;
                }
            }
        }

        private void methodCase4()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("\n------------------------------------------\n" +
                    "0 | Open picture of thompson construction from regex (ab|ba)*(ab)+\n" +
                    "1 | Exit submenu\n");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.NumPad0:
                        openPicture("aborbastarabplus.png");
                        break;
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("\nExiting submenu\n");
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("\nKey not recognized");
                        break;
                }
            }
        }

        private void methodCase5()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("\n------------------------------------------\n" +
                    "0 | Transform NDFA (ab|ba)* to NFA with explanation via tables\n" +
                    "1 | Open (ab|ba)* NDFA\n" +
                    "2 | open (ab|ba)* DFA\n" +
                    "3 | Exit submenu\n");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.NumPad0:
                        Regex a = new Regex('a');
                        Regex b = new Regex('b');
                        Regex ab = a.dot(b);
                        Regex ba = b.dot(a);
                        Regex aborbastar = (ab.or(ba)).star();

                        RegexToNFAConverter rnc = new RegexToNFAConverter("abe");
                        DFAbuilder db = rnc.RegexToNFA(aborbastar);

                        NFAToDFAConverter nfac = new NFAToDFAConverter();
                        DFAbuilder nfa = nfac.NFAToDFA(db);
                        break;
                    case ConsoleKey.NumPad1:
                        openPicture("aborbastar.png");
                        break;
                    case ConsoleKey.NumPad2:
                        openPicture("aborbastardfa.png");
                        break;
                    case ConsoleKey.NumPad3:
                        Console.WriteLine("\nExiting submenu\n");
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("\nKey not recognized");
                        break;
                }
            }
        }

        public string getMenu() {
            return "------------------------------------------\n" +
                "Choose your option using your numpad:\n" +
                "0 | Hardcoded examples of a regex and NDFA (picture)\n" +
                "1 | Generate language from regex (and NOT language)\n" +
                "2 | Test the acceptor for DFA (picture of DFA)\n" +
                "3 | Use specific constructors to create dfa starting with, ending with, or containing a regex\n" +
                "4 | Generate a NDFA from regex using the Thompson constructions (picture of NDFA)\n" +
                "5 | Transform NDFA to NFA with explanation via tables\n" +
                "------------------------------------------\n";
        }

        public void openPicture(string path) {
            string directory = "D://graphvizdiagram//testing//";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/C " + directory + path;
            process.StartInfo = startInfo;
            process.Start();
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
