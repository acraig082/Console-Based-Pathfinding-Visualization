using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraGrid
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 100;
            int height = 20;
            int rooms = 4;

            Level level = new Level(width, height, rooms);
            

            List<Vertex<char>> _vertices = new List<Vertex<char>>();
            List<WeightedEdge<char>> _edges = new List<WeightedEdge<char>>();
            Dictionary<(int, int), Vertex<char>> _dicV = new Dictionary<(int, int), Vertex<char>>();

            // add vertices
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (level._map[i, j] == ' ')
                    {
                        Vertex<char> tempV = new Vertex<char>(level._map[i, j]);
                        (int, int) l = (i, j);
                        tempV.Location = l;
                        _dicV.Add((i, j), tempV);
                    }
                }
            }

            // add neighbors and edges
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    (int, int) temp = (i, j);

                    Console.Write(level._map[i, j]);

                    if (level._map[i, j] == ' ')
                    {
                        //left
                        if (i - 1 >= 0 && level._map[i - 1, j] == ' ')
                        {
                            (int, int) left = (i - 1, j);
                            WeightedEdge<char> e = new WeightedEdge<char>(_dicV[temp], _dicV[left], 1);
                            _edges.Add(e);
                            _dicV[temp].AddEdge(e);


                        }
                        //right
                        if (i + 1 < width && level._map[i + 1, j] == ' ')
                        {
                            (int, int) right = (i + 1, j);
                            WeightedEdge<char> e = new WeightedEdge<char>(_dicV[temp], _dicV[right], 1);
                            _edges.Add(e);
                            _dicV[temp].AddEdge(e);
                        }
                        //down
                        if (j - 1 >= 0 && level._map[i, j - 1] == ' ')
                        {
                            (int, int) down = (i, j - 1);
                            WeightedEdge<char> e = new WeightedEdge<char>(_dicV[temp], _dicV[down], 1);
                            _edges.Add(e);
                            _dicV[temp].AddEdge(e);
                        }
                        //up
                        if (j + 1 < height && level._map[i, j + 1] == ' ')
                        {
                            (int, int) up = (i, j + 1);
                            WeightedEdge<char> e = new WeightedEdge<char>(_dicV[temp], _dicV[up], 1);
                            _edges.Add(e);
                            _dicV[temp].AddEdge(e);
                        }

                        _vertices.Add(_dicV[temp]);

                    }
                }
                Console.WriteLine();
            }

            (int, int) start = (0,0);
            (int, int) end = (width - 1, height - 1);
            string algorithm = "Dijkstra";

            WeightedGraph<char> graph = new WeightedGraph<char>(_vertices, _edges);
            List<Vertex<char>> path = graph.Pathfinder(_dicV[start], _dicV[end], algorithm);

            foreach(Vertex<char> v in path)
            {
                (int, int) l = v.Location;
                level._map[l.Item1, l.Item2] = '*';
            }

            level.displayLevel();


            Console.WriteLine("\nAlgorithm: " + algorithm + "\nHeuristic: random(-1, 1)");
            Console.Read();
        }


    }
}
