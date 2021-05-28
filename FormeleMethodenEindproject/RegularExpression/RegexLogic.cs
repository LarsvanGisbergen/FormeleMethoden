using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FormeleMethodenEindproject.RegularExpression
{
    /// <summary>
    /// Parses the Regex class to view the language it describes.
    /// Does not need to be initialized as it's only used for its methods.
    /// </summary>
    public static class RegexLogic
    {
        

        /// <summary>
        /// Given a valid regex, returns a List<string> containing all possible words in the language (limited to n characters)
        /// </summary>
        public static SortedSet<string> regexToLanguage(Regex regex, int maxLen)
        {
            SortedSet<string> emptyLanguage = new SortedSet<string>();
            SortedSet<string> languageResult = new SortedSet<string>();
            SortedSet<string> languageLeft, languageRight;

            if (maxLen < 1)
            {
                return emptyLanguage;
            }

            switch (regex.op) {
                case Regex.Operator.ONE:
                    if (!regex.terminal.Equals('-'))
                    {
                        languageResult.Add(regex.terminal.ToString());
                    }
                    break;

                case Regex.Operator.OR:
                    languageLeft = regex.left == null ? emptyLanguage : regexToLanguage(regex.left, maxLen-1);
                    languageRight = regex.right == null ? emptyLanguage : regexToLanguage(regex.right, maxLen - 1);                  
                    languageResult.UnionWith(languageLeft);
                    languageResult.UnionWith(languageRight);
                    break;


                case Regex.Operator.DOT:
                    languageLeft = regex.left == null ? emptyLanguage : regexToLanguage(regex.left, maxLen - 1);
                    languageRight = regex.right == null ? emptyLanguage : regexToLanguage(regex.right, maxLen - 1);
                    foreach (String s1 in languageLeft)
                    {
                        foreach (String s2 in languageRight)
                        {
                            languageResult.Add(s1 + s2);
                        }
                    }
                    break;

                case Regex.Operator.STAR:
                case Regex.Operator.PLUS:
                    languageLeft = regex.left == null ? emptyLanguage : regexToLanguage(regex.left, maxLen - 1);
                    languageResult.UnionWith(languageLeft);
                    for (int i = 1; i < maxLen; i++)
                    {
                        HashSet<String> languageTemp = new HashSet<String>(languageResult);
                        foreach (String s1 in languageLeft)
                        {
                            foreach (String s2 in languageTemp)
                            {
                                languageResult.Add(s1 + s2);
                            }
                        }
                    }
                    if (regex.op  == Regex.Operator.STAR)
                    { languageResult.Add(""); }
                    break;


                default:
                    Console.WriteLine("Case unrecognised, ended in default case...");
                    break;
            }


            return languageResult;
        }


    }
}
