using System;
using System.Collections.Generic;
using System.Text;


namespace FormeleMethodenEindproject.RegularExpression
{
    /// <summary>
    /// Defines a single piece of regex which can be part of a larger regex.
    /// </summary>
    public class Regex
    {
        public Regex left;
        public Regex right;
        public Operator op;
        public char terminal;

        public enum Operator { PLUS, STAR, OR, DOT, ONE }

        /// <summary>
        /// terminal can only be a char. To get the regex: abc -> join the character singletons together. 
        /// </summary>
        /// <param name="terminal"></param>
        public Regex()
        {
            this.op = Operator.ONE;
            this.terminal = '-'; //no terminal value
            left = null;
            right = null;
        }
        public Regex(char terminal)
        {
            this.op = Operator.ONE;
            this.terminal = terminal; //character value
            left = null;
            right = null;
        }

        public Regex dot(Regex e2)
        {
            Regex result = new Regex();
            result.op = Operator.DOT;
            result.left = this;
            result.right = e2;
            return result;
        }

        public Regex or(Regex e2)
        {
            Regex result = new Regex();
            result.op = Operator.OR;
            result.left = this;
            result.right = e2;
            return result;
        }

        public Regex star()
        {
            Regex result = new Regex();
            result.op = Operator.STAR;
            result.left = this;
            return result;
        }

        public Regex plus()
        {
            Regex result = new Regex();
            result.op = Operator.PLUS;
            result.left = this;
            return result;
        }
    }



}
