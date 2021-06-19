using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FormeleMethodenEindproject.Graphviz
{
    class Graphbuilder
    {
        private readonly string directory = "D://graphvizdiagram/graph.dot";

        private DFA dfa;

        public Graphbuilder(DFA dfa)
        {
            this.dfa = dfa;
        }

        public async Task createGraph()
        {
            
            List<string> lines = new List<string>(); 
            lines.Add("digraph id {\nrankdir=LR;");


            dfa.Nodes.ForEach(node => {
                string line = "" + node.Id;
                if (node.Begin) {
                    lines.Add("fake" + node.Id + " [style=invisible]\nfake" + node.Id + " -> " + node.Id + " [style=bold]");
                    line += "[root=true]"; 
                }
                if (node.End) { line += "[shape=doublecircle]"; }

                
                lines.Add(line);
            });

            dfa.Transitions.ForEach(t => {
                string line = "" + t.Origin + " -> " + t.Dest;
                if (t.Symbol.Equals("e"))
                    line += "[label=\"ε\"]";
                else
                    line += "[label=\"" + t.Symbol + "\"]";
                
                lines.Add(line);
            });

            lines.Add("}");

            await File.WriteAllLinesAsync(directory, lines);

            drawGraph();
        }

        public void drawGraph() {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/C dot -Tpng " + directory + " -o " + Path.GetFullPath(Path.Combine(directory, @"..\graph.png"));
            process.StartInfo = startInfo;
            process.Start();
            
            System.Diagnostics.Process process2 = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
            startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo2.FileName = "cmd.exe";
            startInfo2.Arguments = @"/C " + Path.GetFullPath(Path.Combine(directory, @"..\graph.png"));
            process2.StartInfo = startInfo2;
            process2.Start();
        }
    }
}
