using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethodenEindproject.Parsers
{
    /// <summary>
    /// Parses the Regex class to view the language it describes.
    /// Does not need to be initialized as it's only used for its methods.
    /// </summary>
    public static class RegexParser
    {
        public enum Operator { PLUS, STAR, OR, DOT }

        /// <summary>
        /// Given a valid regex, returns a List<string> containing all possible words in the language (limited to n characters)
        /// </summary>
        public static List<string> RegexToLanguage(Regex regex, int n)
        {
            List<string> emptyLanguage = new List<string>();

            //to do: return generated language for n maximum characters
            return emptyLanguage;
        }
    }
}
