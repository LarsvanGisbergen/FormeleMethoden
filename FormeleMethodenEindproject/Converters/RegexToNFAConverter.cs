using FormeleMethodenEindproject.RegularExpression;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethodenEindproject.Converters
{
   public class RegexToNFAConverter
    {
        
        public DFAbuilder _dfabuilder;

        public RegexToNFAConverter()
        {
            resetDFABuilder();   
        }

        
        public void resetDFABuilder()
        {
            //TODO: alphabet aanpassen
            this._dfabuilder =  new DFAbuilder("ab");
            this._dfabuilder.addNode(true,false,0);
        }

        public DFAbuilder RegexToNFA(Regex regex)
        {
            resetDFABuilder();
            RegexToNFARecursive(regex);
            this._dfabuilder.addNode(false, true, this._dfabuilder.LastNodeID + 1);
            this._dfabuilder.addTransition(_dfabuilder.LastNodeID - 1, _dfabuilder.LastNodeID, '-');
            return this._dfabuilder;
        }
        private void RegexToNFARecursive(Regex regex)
        {
            switch (regex.op)
            {
                case Regex.Operator.ONE:
                    this._dfabuilder.addNode(false, false, _dfabuilder.LastNodeID + 1);
                    this._dfabuilder.addTransition(_dfabuilder.LastNodeID - 1, _dfabuilder.LastNodeID, regex.terminal);
                    break;

                case Regex.Operator.OR:

                    break;

                case Regex.Operator.DOT:              
                    RegexToNFARecursive(regex.left);
                    RegexToNFARecursive(regex.right);
                    break;

                case Regex.Operator.STAR:
                    break;

                case Regex.Operator.PLUS:
                    break;

                default:
                    Console.WriteLine("Case unrecognised, ended in default case...");
                    break;
            }           
        }

       
    }
}
