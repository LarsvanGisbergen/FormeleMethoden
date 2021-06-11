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
            this._dfabuilder =  new DFAbuilder("abe"); // e = epsilon
            this._dfabuilder.addNode(true,false,0);
        }

        public DFAbuilder RegexToNFA(Regex regex)
        {
            resetDFABuilder();
            RegexToNFARecursive(regex);
            this._dfabuilder.addNode(false, true, this._dfabuilder.LastNodeID + 1);
            this._dfabuilder.addTransition(_dfabuilder.LastNodeID - 1, _dfabuilder.LastNodeID, 'e');
            return this._dfabuilder;
        }
        private void RegexToNFARecursive(Regex regex)
        {
           
            switch (regex.op)
            {
                case Regex.Operator.ONE:
                    this._dfabuilder.addNode(false, false, _dfabuilder.LastNodeID + 1);                  
                    break;

                case Regex.Operator.OR:
                    int tmp = _dfabuilder.LastNodeID;
                    Console.WriteLine("tmp: " + tmp);
                    RegexToNFARecursive(regex.left);
                    int x = _dfabuilder.LastNodeID;
                    Console.WriteLine("x: " + x);
                    RegexToNFARecursive(regex.right);
                    int y = _dfabuilder.LastNodeID;
                    Console.WriteLine("y: " + y);

                    
                    _dfabuilder.addTransition(tmp, tmp + 1, 'e');
                    _dfabuilder.addTransition(tmp, x + 1, 'e');
                   
                    this._dfabuilder.addNode(false, false, y + 1);
                    _dfabuilder.addTransition(x, y + 1, 'e');
                    _dfabuilder.addTransition(y, y + 1, 'e');
                    break;

                case Regex.Operator.DOT:

                    int temp0 = _dfabuilder.LastNodeID; 
                    
                    //Empty node for left epsilon transition
                    this._dfabuilder.addNode(false, false, _dfabuilder.LastNodeID + 1);
                    int temp1 = _dfabuilder.LastNodeID;                   
                    _dfabuilder.addTransition(temp0, temp1, 'e'); 

                    //Recursive left, creates node for left
                    RegexToNFARecursive(regex.left); 
                    int temp2 = _dfabuilder.LastNodeID;                  
                    _dfabuilder.addTransition(temp1, temp2, regex.left.terminal); 

                    //Empty node for right epsilon transition
                    this._dfabuilder.addNode(false, false, _dfabuilder.LastNodeID + 1); 
                    int temp3 = _dfabuilder.LastNodeID;                   

                    //Transition from empty to left
                    _dfabuilder.addTransition(temp2, temp3, 'e'); 

                    //Recursive right creates node for right
                    RegexToNFARecursive(regex.right);

                    int temp4 = _dfabuilder.LastNodeID;                     
                    //Transition from node right 
                    _dfabuilder.addTransition(temp3, temp4, regex.right.terminal); 

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
