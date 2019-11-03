using System.Runtime.CompilerServices;

namespace Graphs
{
    public class Edge<T>
    {
        private Vertex<T> node1;
        private Vertex<T> node2;
        public int Weight { get; set; }

        /*public Edge(Vertex<T> node1, Vertex<T> node2) : this (node1, node2, 0) {}

        public Edge(Vertex<T> node1, Vertex<T> node2, int weight)
        {
            this.node1 = node1;
            this.node2 = node2;
            Weight = weight;
        }
        
        public Edge() {}*/
    }
}