using System.Collections.Generic;

namespace Graphs
{
    public class Vertex<T>
    {
        public char Label { get; }
        public bool Visited { get; set; }
        public T Data { get; }
        public LinkedList<Vertex<T>> neighbors { get; set; } // это те вершины, с которыми эта вершина связана

        public Vertex(T data)
        {
            Visited = false;
            Data = data;
        }
    }
}