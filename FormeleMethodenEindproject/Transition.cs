using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethodenEindproject
{
    class Transition
    {
        private int origin;
        private int dest;
        char symbol;

        public Transition(int origin, int dest, char symbol)
        {
            this.origin = origin;
            this.dest = dest;
            this.symbol = symbol;
        }

        public char Symbol { get => symbol; }
        internal int Origin { get => origin; }
        internal int Dest { get => dest; }
    }
}
