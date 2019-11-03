using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    public class Graph<T>
    {
        private Vertex<T>[] vertexList;
        //private Edge<T>[] edgeList;
        private int MaxVerts;
        //Параметризовал элементы adjList кортежем, где первое число - номер, второе - вес. Вершина сама с собой - вес 0. 
        private LinkedList<Tuple<int, int>>[] adjList;

        public Graph(int n)
        {
            vertexList = new Vertex<T>[n];
            adjList = new LinkedList<Tuple<int, int>>[n];
            for (int i = 0; i < adjList.Length; i++)
            {
                adjList[i] = new LinkedList<Tuple<int, int>>();
            }
            NumberOfVertices = 0;
            MaxVerts = n;
        }
        
        public Graph(int n, bool weighted)
        {
            vertexList = new Vertex<T>[n];
            adjList = new LinkedList<Tuple<int, int>>[n];
            for (int i = 0; i < adjList.Length; i++)
            {
                adjList[i] = new LinkedList<Tuple<int, int>>();
            }
            NumberOfVertices = 0;
            MaxVerts = n;

            if (weighted)
            {
                
            }
        }

        public virtual void AddVertex(T data)
        {
            vertexList[NumberOfVertices++] = new Vertex<T>(data);
        }

        public virtual void AddEdge(int start, int end) 
        {
            adjList[start].AddLast(Tuple.Create(end, 0));
        }
        
        public virtual void AddEdge(int start, int end, int weight) //с верхним методом сделйа так, чтобы в кортеже второе число было ноль, а тут weight
        {
            adjList[start].AddLast(Tuple.Create(end, weight));
        }

        public virtual void DisplayGraph()
        {
            for (int v = 0; v < vertexList.Length; v++)
            {
                DisplayVertex(v);
            }
        }

        public virtual void DisplayVertex(int v)
        {
            Console.WriteLine("Data: {0}", vertexList[v].Data.ToString());
            Console.WriteLine();
        }
        
        public virtual T GetData(int v)
        {
            return vertexList[v].Data;
        }
        
        public int NumberOfVertices { get; set; }
        
        public int NumberOfEdges
        {
            get;
            set;
        }

        public void DFS(int s) //параметр указывает, с какой вершины начинаем
        {
            Stack<int> stack = new Stack<int>(); //сюда будем класть индексы вершин и массива вершин vertexList
            vertexList[s].Visited = true;
            DisplayVertex(s);
            stack.Push(s); // индекс из массива вершин в качестве параметра

            while (stack.Count > 0)
            {
                int v = GetIndexOfUnvisitedVertex(stack.Peek());
                if (v == -1)
                {
                    stack.Pop();
                }
                else
                {
                    vertexList[v].Visited = true;
                    DisplayVertex(v);
                    stack.Push(v);
                }
            }
            
            foreach (Vertex<T> vertex in vertexList)
            {
                if (vertex != null)
                {
                    vertex.Visited = false;
                }
            }
        }

        protected virtual int GetIndexOfUnvisitedVertex(int v)
        {
            
                for (int i = 0; i < NumberOfVertices; i++)
                {
                    if (adjList[v].Contains(i) && vertexList[i].Visited == false)
                    {
                        return i;
                    }
                }

                return -1;
        }
        
        public void BFS(int s)
        {
        Queue<int> queue = new Queue<int>(); //очередь для BFS, хранит номера вершин
        vertexList[s].Visited = true;
        queue.Enqueue(s);

        while (queue.Count() != 0)
        {
            s = queue.Dequeue();
            DisplayVertex(s);

            IEnumerator<Tuple<int, int>> ie = adjList[s].GetEnumerator(); //нельзя менять коллекцию после создания перечислителя
            for (int i = 0; i < adjList.Length; i++)
            {
                while (ie.MoveNext())
                {
                    int n = ie.Current;
                    if (!vertexList[n].Visited)
                    {
                        vertexList[n].Visited = true;
                        queue.Enqueue(n);
                    }
                    //ie.MoveNext();
                }
            }
            ie.Dispose(); //?
        }

        foreach (Vertex<T> vertex in vertexList)
        {
            if (vertex != null)
            {
                vertex.Visited = false;
            }
        }
        }

        public void Dijkstra() //подумай про параметры. Как именно указать source
        {
            int[,] graph = new int[vertexList.Length, vertexList.Length];
            
            foreach (LinkedList<Tuple<int, int>> element in adjList) //если связи между вершинами нет, то вес = 0
            {
                for (int i = 0; i < element.Count; i++)
                {
                    if (element.Contains(i))
                    {
                        //graph[element,i] = weight //как задать тут вес?? 
                    }
                }
            } 
        }

        /*public void Dijkstra(int [,] graph, int source)
        {
            int[] dist = new int [graph.Length]; //элементы содержат кратчайший путь из начала в вершину
            
            
            // sptSet[i] will true if vertex i is included in shortest path 
            // tree or shortest distance from 
            // src to i is finalized  
            bool[] sptSet = new bool[graph.Length];

            for (int i = 0; i < graph.Length; i++)
            {
                dist[i] = Int32.MaxValue;
            }

            dist[source] = 0;

            for (int i = 0; i < graph.Length; i++) // до Length - 1 ??
            {
                int u = MinDistance(dist, sptSet);
                sptSet[u] = true;

                for (int v = 0; v < graph.Length; v++)
                {
                    if (!sptSet[u] && graph[u, v] != 0 && dist[u] != Int32.MaxValue && dist[u] + graph[u, v] < dist[v])
                    {
                        dist[v] = dist[u] + graph[u, v];
                    }
                }
                PrintSolution(dist);
            }

        }

        private int MinDistance(int[] dist, bool[] sptSet)
        {
            int min = Int32.MaxValue, minIndex = -1;

            for (int v = 0; v < sptSet.Length; v++) //sptSet.Length == graph.Length
            {
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    minIndex = v;
                }
            }
            return minIndex;
        }

        private void PrintSolution(int[] dist)
        { 
            Console.Write("Vertex \t\t Distance "
                          + "from Source\n"); 
            for (int i = 0; i < dist.Length; i++) 
                Console.Write(i + " \t\t " + dist[i] + "\n"); 
        } */
    }
}