using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    public class Graph<T>
    {
        private List<Vertex<T>> vertexList;
        public int NumberOfVertices { get; set; }
        public int NumberOfEdges { get; set; }
        private int MaxVerts;
        //adjList - массив списков
        //Параметризовал элементы adjList кортежем, где первое число - номер, второе - вес. Вершина сама с собой - вес 0. 
        private List<Tuple<int, int>>[] adjList;

        public Graph(int n)
        {
            vertexList = new List<Vertex<T>>();
            adjList = new List<Tuple<int, int>>[n];
            for (int i = 0; i < adjList.Length; i++)
            {
                adjList[i] = new List<Tuple<int, int>>();
            }
            NumberOfVertices = 0;
            MaxVerts = n;
        }

        public virtual void AddVertex(T data)
        {
            vertexList.Insert(NumberOfVertices++, new Vertex<T>(data));
        }

        //Добавление происходит как для ориентированного графа
        //Если граф не ориентированный, нужно отдельно применить метод AddEdge, поменяв первые 2 индекса местами
        
        public virtual void AddEdge(int start, int end) 
        {
            //Если граф не взвешенный, то ставим вес = 0
            adjList[start].Add(Tuple.Create(end, 0)); 
            adjList[end].Add(Tuple.Create(start, 0)); 
        }
        
        public virtual void AddEdge(int start, int end, int weight)
        {
            if (weight < 0) throw new ArgumentException();

            adjList[start].Add(Tuple.Create(end, weight));
            adjList[end].Add(Tuple.Create(start, weight));
        }

        public virtual void DeleteVertex(int number)
        {
            vertexList.RemoveAt(number);
        }

        public virtual void DeleteEdge(int start, int end)
        {
            adjList[start].RemoveAt(end);
        }

        public virtual void DisplayGraph()
        {
            for (int v = 0; v < NumberOfVertices; v++) //было vertexList.Count
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
                    if (adjList[v].Contains(Tuple.Create(i, 0)) && vertexList[i].Visited == false) //вес ставлю 0, тк граф не взвешанный в данном случае
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
                    var (counter, weight) = ie.Current;
                    if (!vertexList[counter].Visited)
                    {
                        vertexList[counter].Visited = true;
                        queue.Enqueue(counter);
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

        public void Dijkstra(int source) 
        {

            bool[] sptSet = new bool[NumberOfVertices]; // Массив, где содержится информация, нашли ли мы минимальный путь до этой вершины
            int[] dist = new int[NumberOfVertices]; // Массив расстояний до вершины от source

            for (int i = 0; i < NumberOfVertices; i++)
            {
                dist[i] = Int32.MaxValue;
            }

            dist[source] = 0;

            for (int count = 0; count < NumberOfVertices; count++) //NumberOfVertices - 1 ??
            {

                int u = MinDistance(dist, sptSet);
                
                    sptSet[u] = true;
                    
                    foreach (Tuple<int, int> element in adjList[u])
                    {
                        if (!sptSet[element.Item1] && dist[u] != Int32.MaxValue && dist[u] + element.Item2 < dist[element.Item1])
                            
                            dist[element.Item1] = dist[u] + element.Item2;
                    }
            }
            
            PrintDijkstra(dist);
        }

        //Этот метод возвращает индекс вершины, до которой не найдено минимальный путь из возможных
        private int MinDistance(int[] dist, bool[] sptSet)
        {
            int min = Int32.MaxValue;
            int minIndex = -1;
            
            for (int u = 0; u < NumberOfVertices; u++)
            {
                if (sptSet[u] == false && dist[u] <= min)
                {
                    min = dist[u];
                    minIndex = u;
                }
            }

            return minIndex;
        }

        private void PrintDijkstra(int[] dist)
        {
            Console.Write("Vertex \t\t Distance from Source\n"); 
            
            for (int i = 0; i < NumberOfVertices; i++) 
                Console.Write(i + " \t\t " + dist[i] + "\n"); 
        }
    }
}