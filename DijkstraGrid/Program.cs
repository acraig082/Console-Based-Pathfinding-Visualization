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
            Dictionary<Location, Vertex<char>> _dicV = new Dictionary<Location, Vertex<char>>();

            // add vertices
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (level._map[i, j] == ' ')
                    {
                        Vertex<char> tempV = new Vertex<char>(level._map[i, j]);
                        Location l = new Location(i, j);
                        tempV.Location = l;
                        _dicV.Add(l, tempV);
                    }
                }
            }

            // add neighbors and edges
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Location temp = new Location(i, j);

                    //left
                    if (i - 1 >= 0 && level._map[i - 1, j] == ' ')
                    {
                        Location left = new Location(i - 1, j);
                        WeightedEdge<char> e = new WeightedEdge<char>(_dicV[temp], _dicV[left], 1);
                        _edges.Add(e);
                        _dicV[temp].AddEdge(e);
                        

                    }
                    //right
                    if (i + 1 <= width && level._map[i + 1, j] == ' ')
                    {
                        Location right = new Location(i + 1, j);
                        WeightedEdge<char> e = new WeightedEdge<char>(_dicV[temp], _dicV[right], 1);
                        _edges.Add(e);
                        _dicV[temp].AddEdge(e);
                    }
                    //down
                    if (j - 1 >= 0 && level._map[i, j - 1] == ' ')
                    {
                        Location down = new Location(i, j - 1);
                        WeightedEdge<char> e = new WeightedEdge<char>(_dicV[temp], _dicV[down], 1);
                        _edges.Add(e);
                        _dicV[temp].AddEdge(e);
                    }
                    //up
                    if (j + 1 <= height && level._map[i, j + 1] == ' ')
                    {
                        Location up = new Location(i, j + 1);
                        WeightedEdge<char> e = new WeightedEdge<char>(_dicV[temp], _dicV[up], 1);
                        _edges.Add(e);
                        _dicV[temp].AddEdge(e);
                    }

                    _vertices.Add(_dicV[temp]);

                }
            }

            Location start = new Location(0,0);
            Location end = new Location(width - 1, height - 1);
            string algorithm = "Dijkstra";

            WeightedGraph<char> graph = new WeightedGraph<char>(_vertices, _edges);
            List<Vertex<char>> path = graph.Pathfinder(_dicV[start], _dicV[end], algorithm);

            foreach(Vertex<char> v in path)
            {
                Location l = v.Location;
                level._map[l.getX(), l.getY()] = '*';
            }

            level.displayLevel();

            Console.Read();
        }


    }
}
