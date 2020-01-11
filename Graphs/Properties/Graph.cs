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
        
        public virtual void AddEdge(int start, int end, bool directed) 
        {
            //Если граф не взвешенный, то ставим вес = 0
            
            if (directed) adjList[start].Add(Tuple.Create(end, 0));
            else
            {
                adjList[start].Add(Tuple.Create(end, 0));
                adjList[end].Add(Tuple.Create(start, 0));
            }
        }
        
        public virtual void AddEdge(int start, int end, int weight, bool directed)
        {
            if (weight < 0) throw new ArgumentException();

            if (directed) adjList[start].Add(Tuple.Create(end, weight));
            else
            {
                adjList[start].Add(Tuple.Create(end, weight));
                adjList[end].Add(Tuple.Create(start, weight));
            }
        }

        public virtual void DeleteVertex(int number)
        {
            vertexList.RemoveAt(number);
        }

        public virtual void DeleteEdge(int start, int end, bool directed)
        {
            if (directed) adjList[start].RemoveAt(end);
            else
            {
                adjList[start].RemoveAt(end);
                adjList[end].RemoveAt(start);
            }
        }

        public virtual void DisplayGraph()
        {
            for (int v = 0; v < NumberOfVertices; v++)
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
            Stack<int> stack = new Stack<int>(); //сюда будем класть индексы вершин из массива вершин vertexList
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
            
            //сбрасываем состояние графа до начального, т.е. все вершины не посещены
            foreach (Vertex<T> vertex in vertexList)
            {
                if (vertex != null)
                {
                    vertex.Visited = false;
                }
            }
        }

        //по данному индексу ищем вершины, которые инцидентны с вершиной по данному индексу и не посещены
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
            
             //В прошлых версиях здесь зачем-то был цикл for
             
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
                    
                    foreach (Tuple<int, int> element in adjList[u]) //цикл по каждой вершине, инцидентной u
                    {
                        if (!sptSet[element.Item1] && dist[u] != Int32.MaxValue && dist[u] + element.Item2 < dist[element.Item1])
                            
                            dist[element.Item1] = dist[u] + element.Item2;
                    }
            }
            
            PrintDijkstra(dist);
        }

        //Этот метод возвращает индекс вершины, до которой не найден минимальный путь из возможных и до которой минимальное расстояние
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

        public void PrimMST()
        {
            // Making an adj matrix
            int[,] graph = new int[NumberOfVertices, NumberOfVertices];
            
            
            for (int i = 0; i < adjList.Length; i++)
            {
                foreach (var vertex in adjList[i])
                {
                    int j = vertex.ToValueTuple().Item1;
                    int weight = vertex.ToValueTuple().Item2;
                    graph[i, j] = weight;
                }
            }
            
            // Array to store constructed MST 
            int[] parents = new int[NumberOfVertices];
            
            // Key values used to pick 
            // minimum weight edge in cut 
            int[] keys = new int[NumberOfVertices];
            
            // To represent set of vertices 
            // not yet included in MST
            bool[] MSTSet = new bool[NumberOfVertices];
            
            int[] weigths = new int[NumberOfEdges];

            // Initialize all keys 
            // as int.MaxValue 
            for (int i = 0; i < NumberOfVertices; i++) { 
                keys[i] = int.MaxValue; 
                MSTSet[i] = false; 
            } 
            
            // Always include first 1st vertex in MST. 
            // Make key 0 so that this vertex is 
            // picked as first vertex 
            // First node is always root of MST 
            keys[0] = 0; 
            parents[0] = -1; 
            
            // The MST will have V vertices 
            for (int count = 0; count < NumberOfVertices - 1; count++)
            {
                // Pick thd minimum key vertex 
                // from the set of vertices 
                // not yet included in MST 
                int u = minKey(keys, MSTSet);
                
                // Add the picked vertex 
                // to the MST Set 
                MSTSet[u] = true; 
                
                // Update key value and parent 
                // index of the adjacent vertices 
                // of the picked vertex. Consider 
                // only those vertices which are 
                // not yet included in MST 
                for (int v = 0; v < NumberOfVertices; v++)
                {
                    // adjList[u].Contains(Tuple.Create(v, weight) 
                        // only for adjacent vertices of m 
                        // mstSet[v] is false for vertices 
                        // not yet included in MST Update 
                        // the key only if graph[u][v] is 
                        // smaller than key[v] 
                    
                        if (graph[u, v] != 0 && MSTSet[v] == false && graph[u, v] < keys[v])
                        {
                            parents[v] = u;
                            keys[v] = graph[u, v];
                        }
                }
                printPrimMST(parents, graph);
            }
        }
        
        // A utility function to find 
        // the vertex with minimum key 
        // value, from the set of vertices 
        // not yet included in MST 
        private int minKey(int[] keys, bool[] MSTSet) 
        { 
  
            // Initialize min value 
            int min = int.MaxValue, min_index = -1; 
  
            for (int v = 0; v < NumberOfVertices; v++) 
                if (MSTSet[v] == false && keys[v] < min) { 
                    min = keys[v]; 
                    min_index = v; 
                } 
  
            return min_index; 
        }

        private void printPrimMST(int[] parent, int[, ] graph) 
        { 
            Console.WriteLine("Edge \tWeight"); 
            for (int i = 1; i < NumberOfVertices; i++) 
                Console.WriteLine(parent[i] + " - " + i + "\t" + graph[i, parent[i]]); 
        } 
                        
    }
}