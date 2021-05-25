using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethodenEindproject
{
    
    class Node
    {
        private bool begin;
        private bool end;
        private int id;

       
        public Node(bool begin, bool end, int id)
        {
            this.begin = begin;
            this.end = end;
            this.id = id;
        }

        
        public bool Begin { get => begin;}
        public bool End { get => end; }
        public int Id { get => id; }
    }
}
