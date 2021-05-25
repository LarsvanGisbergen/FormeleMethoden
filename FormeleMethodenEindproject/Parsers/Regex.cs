using System;
using System.Collections.Generic;
using System.Text;


namespace FormeleMethodenEindproject.Parsers
{
    /// <summary>
    /// Defines how Regex should be constructed so it can be used in the RegexParser.
    /// Consists of a List containing regex_operators as enums.
    /// </summary>
    public class Regex
    {

        public List<RegexParser.Operator> regex_operators;
        public Regex(string regex)
        {
            this.regex_operators = new List<RegexParser.Operator>();
        }

        /// <summary>
        /// extract operators in the correct order from regex as string and place them in regex_operators as enums.
        /// </summary>
        private void extractOperators(string regex)
        {
            //to do: extract each operator and place them in regex_operators
        }
    }



}
