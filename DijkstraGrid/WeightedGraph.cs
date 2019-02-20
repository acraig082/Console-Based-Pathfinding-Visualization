using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraGrid
{
    class WeightedGraph<T>
    {
        List<WeightedEdge<T>> edges;
        List<Vertex<T>> vertices;
        public List<WeightedEdge<T>> Edges { get { return edges; } }
        Random rnd = new Random();

        public WeightedGraph(List<Vertex<T>> vertices, List<WeightedEdge<T>> edges)
        {
            this.vertices = vertices;
            this.edges = edges;
        }
        public void AddEdge(WeightedEdge<T> newEdge)
        {
            edges.Add(newEdge);
        }
        public void RemoveEdge(WeightedEdge<T> edge)
        {
            edges.Remove(edge);
        }

        /// <summary>
        /// Pathfinding algorithms available: Dijkstra and AStar
        /// </summary>
        public List<Vertex<T>> Pathfinder(Vertex<T> start, Vertex<T> end, string algorithm)
        {
            Func<Vertex<T>, Vertex<T>, List<Vertex<T>>> pathfinder;

            if (algorithm == "Dijkstra")
            {
                pathfinder = DijkstraSearch;
            }
            else if (algorithm == "AStar")
            {
                pathfinder = AStarSearch;
            }
            else
            {
                throw new ArgumentException("Pathfinding algorithm not available.");
            }
            return pathfinder(start, end);
        }


        public List<Vertex<T>> DijkstraSearch(Vertex<T> start, Vertex<T> end)
        {
            Dictionary<Vertex<T>, Vertex<T>> parentMap = new Dictionary<Vertex<T>, Vertex<T>>();
            PriorityQueue<Vertex<T>> priorityQueue = new PriorityQueue<Vertex<T>>();

            InitializeCosts(start);
            priorityQueue.Enqueue(start, start.Cost);

            Vertex<T> current;

            while (priorityQueue.Count() > 0)
            {

                 current = (Vertex<T>)priorityQueue.Dequeue();

                if (!current.IsVisited)
                {
                    current.IsVisited = true;

                    if (current.Equals(end))
                    {
                        break;
                    }

                    foreach (WeightedEdge<T> edge in current.Edges)
                    {
                        Vertex<T> neighbor = edge.End;

                        int newCost = current.Cost + edge.Weight;
                        int neighborCost = neighbor.Cost;

                        if (newCost < neighborCost)
                        {
                            neighbor.Cost = newCost;
                            parentMap.Add(neighbor, current);
                            int priority = newCost;
                            priorityQueue.Enqueue(neighbor, priority);
                        }
                    }
                }
            }
            List<Vertex<T>> path = ReconstructPath(parentMap, start, end);
            return path;
        }

        public List<Vertex<T>> AStarSearch(Vertex<T> start, Vertex<T> end)
        {
            Dictionary<Vertex<T>, Vertex<T>> parentMap = new Dictionary<Vertex<T>, Vertex<T>>();
            PriorityQueue<Vertex<T>> priorityQueue = new PriorityQueue<Vertex<T>>();

            InitializeCosts(start);
            priorityQueue.Enqueue(start, start.Cost);

            Vertex<T> current;

            while (priorityQueue.Count() > 0)
            {
                current = (Vertex<T>)priorityQueue.Dequeue();

                if (!current.IsVisited)
                {
                    current.IsVisited = true;

                    if (current.Equals(end))
                    {
                        break;
                    }

                    foreach (WeightedEdge<T> edge in current.Edges)
                    {
                        Vertex<T> neighbor = edge.End;

                        int newCost = current.Cost + edge.Weight;
                        int neighborCost = neighbor.Cost;

                        if (newCost < neighborCost)
                        {
                            neighbor.Cost = newCost;
                            if (parentMap.ContainsKey(neighbor))
                            {
                                parentMap.Remove(neighbor);
                            }
                            parentMap.Add(neighbor, current);
                            int priority = newCost + Heuristic(neighbor, end);
                            priorityQueue.Enqueue(neighbor, priority);
                        }
                    }
                }
            }
            List<Vertex<T>> path = ReconstructPath(parentMap, start, end);
            return path;
        }

        public int Heuristic(Vertex<T> vertexA, Vertex<T> vertexB)
        {
            int x1 = vertexA.Location.Item1;
            int y1 = vertexA.Location.Item2;
            int x2 = vertexB.Location.Item1;
            int y2 = vertexB.Location.Item2;

            int x = Math.Abs(x1 - x2);
            int y = Math.Abs(y1 - y2);

            double xsquared = x * x;
            double ysquared = y * y;

            double r = Math.Sqrt(xsquared + ysquared);
            int distance = Convert.ToInt32(r);

            distance += rnd.Next(-4, 4);
            return distance;
        }

        public void InitializeCosts(Vertex<T> start)
        {
            foreach (Vertex<T> vertex in vertices)
            {
                vertex.Cost = int.MaxValue;
            }

            start.Cost = 0;
        }

        public List<Vertex<T>> ReconstructPath(Dictionary<Vertex<T>, Vertex<T>> parentMap, Vertex<T> start, Vertex<T> end)
        {
            List<Vertex<T>> path = new List<Vertex<T>>();
            Vertex<T> current = end;

            while (current != start)
            {
                path.Add(current);
                current = parentMap[current];
            }

            path.Add(start);

            path.Reverse();
            return path;
        }

    }
}
