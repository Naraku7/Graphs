using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Policy;

namespace Graphs
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*Graph<int> graph = new Graph<int>(10);
            graph.AddVertex(1);
            graph.AddVertex(11);
            graph.AddVertex(13);
            graph.AddVertex(90);
            graph.AddVertex(-1);
            graph.AddEdge(0, 1); // 1-2
            graph.AddEdge(1, 4); // 2-5
            graph.AddEdge(4, 2); // 5-3
            graph.AddEdge(2, 3); // 3-4
            Console.WriteLine("DFS of {0}", graph);
            graph.DFS(0);
            Console.WriteLine("BFS of {0}", graph);
            graph.BFS(0);
            
            Graph<String> strGraph = new Graph<string>(6);
            strGraph.AddVertex("a");
            strGraph.AddVertex("b");
            strGraph.AddVertex("c");
            strGraph.AddVertex("d");
            strGraph.AddVertex("e");
            strGraph.AddEdge(0, 1); // 1-2
            strGraph.AddEdge(1, 4); // 2-5
            strGraph.AddEdge(4, 2); // 5-3
            strGraph.AddEdge(2, 3); // 3-4
            Console.WriteLine("{0} DFS", strGraph);
            strGraph.DFS(0);*/
            
            /*Graph<int> g3 = new Graph<int>(5); 
            g3.AddVertex(0); //0
            g3.AddVertex(1); //1
            g3.AddVertex(2); //2
            g3.AddVertex(3); //3
            
            g3.AddEdge(0, 1, false); 

            g3.AddEdge(0, 2, false);

            g3.AddEdge(1, 2, false);
            
            g3.AddEdge(2, 3, false);
            
            g3.AddEdge(3, 3, false);

            Console.WriteLine("g3 BFS");
            g3.BFS(2); // c a b d*/
            
            /*Graph<char> g4 = new Graph<char>(5); 
            g4.AddVertex('a'); //0
            g4.AddVertex('b'); //1
            g4.AddVertex('c'); //2
            g4.AddVertex('d'); //3
            g4.AddEdge(0, 1, false); 
            
            g4.AddEdge(0, 2, false);

            g4.AddEdge(1, 2, false); 
            
            g4.AddEdge(2, 3, false);
            
            g4.AddEdge(3, 3, false);
            Console.WriteLine("g4 BFS");
            g4.BFS(2); */
            
            Graph<char> g5 = new Graph<char>(6); 
            g5.AddVertex('a'); //0
            g5.AddVertex('b'); //1
            g5.AddVertex('c'); //2
            g5.AddVertex('d'); //3
            g5.AddVertex('e'); //4
            g5.AddVertex('f'); //5
            
            g5.AddEdge(0, 1, 2, false); 
            g5.AddEdge(1, -0, 2, false); 
            
            g5.AddEdge(0, 2, 5, false); 
            g5.AddEdge(2, 0, 5, false); 
            
            
            g5.AddEdge(1, 2, 9, false);
            g5.AddEdge(2, 1, 9, false);
            
            g5.AddEdge(2, 3, 3, false);
            g5.AddEdge(3, 2, 3, false);
            
            
            g5.AddEdge(3, 4, 2, false);
            g5.AddEdge(4, 3, 2, false);
            
            g5.AddEdge(4, 5, 3, false);
            g5.AddEdge(5, 4, 3, false);
            
            g5.AddEdge(0, 4, 4, false);
            g5.AddEdge(4, 0, 4, false);
            
            g5.AddEdge(0, 5, 10, false);
            g5.AddEdge(5, 0, 10, false);
            
            //Console.WriteLine("g5 Dijkstra");
            //g5.Dijkstra(1); 
            g5.PrimMST();
            
        }
    }
}