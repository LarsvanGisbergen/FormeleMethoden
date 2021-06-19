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
            if (regex.op == Regex.Operator.ONE)
            {
                this._dfabuilder.addNode(false, false, this._dfabuilder.LastNodeID + 1);
                this._dfabuilder.addTransition(_dfabuilder.LastNodeID - 1, _dfabuilder.LastNodeID, regex.terminal);
            }
            else
            {
                RegexToNFARecursive(regex);
            }
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
                    int tmp = _dfabuilder.LastNodeID; // 0
                    //Console.WriteLine("tmp: " + tmp);
                    RegexToNFARecursive(regex.left);
                    if (regex.left.op == Regex.Operator.ONE) {
                        this._dfabuilder.addNode(false, false, _dfabuilder.LastNodeID + 1);
                        _dfabuilder.addTransition(_dfabuilder.LastNodeID -1, _dfabuilder.LastNodeID, regex.left.terminal);
                    }
                    int x = _dfabuilder.LastNodeID; // 1
                    //Console.WriteLine("x: " + x);
                    RegexToNFARecursive(regex.right);
                    if (regex.right.op == Regex.Operator.ONE)
                    {
                        this._dfabuilder.addNode(false, false, _dfabuilder.LastNodeID + 1);
                        _dfabuilder.addTransition(_dfabuilder.LastNodeID - 1, _dfabuilder.LastNodeID, regex.right.terminal);
                    }
                    int y = _dfabuilder.LastNodeID; // 2
                    //Console.WriteLine("y: " + y);
                    
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
                    int st0 = _dfabuilder.LastNodeID; // 0
                    //Console.WriteLine("st0: " +  st0);
                    RegexToNFARecursive(regex.left);
                    if (regex.left.op == Regex.Operator.ONE)
                    {
                        this._dfabuilder.addNode(false, false, _dfabuilder.LastNodeID + 1);
                        _dfabuilder.addTransition(_dfabuilder.LastNodeID - 1, _dfabuilder.LastNodeID, regex.left.terminal);
                    }
                    int st1 = _dfabuilder.LastNodeID; // 4
                    //Console.WriteLine("st1: " + st1);

                    _dfabuilder.addTransition(st0, st0 + 1, 'e'); // Add epsilon transition in the beginning
                    _dfabuilder.addTransition(st1, st0, 'e'); // Add epsilon transition back for star
                    this._dfabuilder.addNode(false, false, _dfabuilder.LastNodeID + 1); // create end node
                    _dfabuilder.addTransition(_dfabuilder.LastNodeID -1, _dfabuilder.LastNodeID, 'e'); //Add epsilon transition from latest recursive to last star node
                    _dfabuilder.addTransition(st0, _dfabuilder.LastNodeID, 'e'); //Add epsilon transition from first node to end node to skip star

                    break;

                case Regex.Operator.PLUS:
                    int pt0 = _dfabuilder.LastNodeID; // 0
                    //Console.WriteLine("st0: " + pt0);
                    RegexToNFARecursive(regex.left);
                    if (regex.left.op == Regex.Operator.ONE)
                    {
                        this._dfabuilder.addNode(false, false, _dfabuilder.LastNodeID + 1);
                        _dfabuilder.addTransition(_dfabuilder.LastNodeID - 1, _dfabuilder.LastNodeID, regex.left.terminal);
                    }
                    int pt1 = _dfabuilder.LastNodeID; // 4
                    //Console.WriteLine("st1: " + pt1);

                    _dfabuilder.addTransition(pt0, pt0 + 1, 'e'); // Add epsilon transition in the beginning
                    _dfabuilder.addTransition(pt1, pt0, 'e'); // Add epsilon transition back for plus
                    this._dfabuilder.addNode(false, false, _dfabuilder.LastNodeID + 1); // create end node
                    _dfabuilder.addTransition(_dfabuilder.LastNodeID - 1, _dfabuilder.LastNodeID, 'e'); //Add epsilon transition from latest recursive to last plus node
                    break;

                default:
                    Console.WriteLine("Case unrecognised, ended in default case...");
                    break;
            }           
        }

       
    }
}
