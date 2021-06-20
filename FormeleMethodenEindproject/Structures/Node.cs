using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethodenEindproject
{
    
    public class Node
    {
        private bool begin;
        private bool end;
        private int id;
        private string name;

       
        public Node(bool begin, bool end, int id)
        {
            this.begin = begin;
            this.end = end;
            this.id = id;
            this.name = "" + id;
        }

        public Node(bool begin, bool end, int id, string name) : this(begin, end, id)
        {
            this.name = name;
        }

        public bool Begin { get => begin; set => begin = value;}
        public bool End { get => end; set => end = value; }
        public int Id { get => id; }
        public string Name { get => name; }
    }
}
