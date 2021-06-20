using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FormeleMethodenEindproject.Graphviz
{
    class Graphbuilder
    {
        private string directory = "D://graphvizdiagram//";

        private DFAbuilder dfa;
        private string filename;

        public Graphbuilder(DFAbuilder dfa)
        {
            this.dfa = dfa;
            this.directory += "graph";
        }

        public Graphbuilder(DFAbuilder dfa, string filename)
        {
            this.dfa = dfa;
            this.directory += filename;
        }

        public async Task createGraph()
        {
            
            List<string> lines = new List<string>(); 
            lines.Add("digraph id {\nrankdir=LR;");

            dfa.getNodes().ForEach(node => {
                string line = "\"" + node.Name + "\"";
                if (node.Begin) {
                    lines.Add("fake" + node.Id + " [style=invisible]\nfake" + node.Id + " -> " + node.Id + " [style=bold]");
                    line += "[root=true]"; 
                }
                if (node.End) { line += "[shape=doublecircle]"; }

                
                lines.Add(line);
            });

            dfa.getTransitions().ForEach(t => {
                string line = "\"" + dfa.getNodeFromId(t.Origin).Name + "\" -> \"" + dfa.getNodeFromId(t.Dest).Name + "\"";
                if (t.Symbol.Equals('e'))
                    line += "[label=\"ε\"]";
                else
                    line += "[label=\"" + t.Symbol + "\"]";
                
                lines.Add(line);
            });

            lines.Add("}");
            await File.WriteAllLinesAsync(directory + ".dot", lines);

            drawGraph();
        }

        public void drawGraph() {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/C dot -Tpng " + directory + ".dot" + " -o " + directory + ".png";
            process.StartInfo = startInfo;
            process.Start();

            Thread.Sleep(500);
            System.Diagnostics.Process process2 = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
            startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo2.FileName = "cmd.exe";
            startInfo2.Arguments = @"/C " + directory + ".png";
            process2.StartInfo = startInfo2;
            process2.Start();
        }
    }
}
