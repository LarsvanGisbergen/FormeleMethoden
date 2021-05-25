﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethodenEindproject
{
    class DFA
    {

        private char[] alphabet;
        private List<Node> nodes;
        private List<Transition> transitions;

        public char[] Alphabet { get => alphabet; }
        internal List<Node> Nodes { get => nodes; }
        internal List<Transition> Transitions { get => transitions; }
    }
}
