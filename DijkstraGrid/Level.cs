using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraGrid
{

    public class Level
    {
        public int _width;
        public int _height;
        public int _rooms;

        public (int, int) _start;
        public (int, int) _end;

        public List<Vertex<char>> _vertices = new List<Vertex<char>>();
        public List<WeightedEdge<char>> _edges = new List<WeightedEdge<char>>();
        public Dictionary<(int, int), Vertex<char>> _vertexDictionary = new Dictionary<(int, int), Vertex<char>>();

        public char[,] _map;

        Random rnd = new Random();

        public Level(int width, int height, int rooms, char[,] map )
        {
            _width = width;
            _height = height;
            _rooms = rooms;

            _start = (1, 1);
            _end = (width - 2, height - 2);

            _map = map;
        }

        public Level(int width, int height, int rooms)
        {
            _width = width;
            _height = height;
            _rooms = rooms;

            _start = (1, 1);
            _end = (width - 2, height - 2);

            _map = new char[width, height];

        }

        public void InitializeLevel()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _map[i, j] = ' ';
                }
            }
        }

        public void AddRandomRooms()
        {
            int partitionWidth = _width / _rooms;
            int count = 1;

            while (count <= _rooms)
            {
                int width = rnd.Next(5, partitionWidth - 2);
                int height = rnd.Next(4, _height - 2);
                int x = rnd.Next(0, (partitionWidth - 1 - width));
                int y = rnd.Next(0, (_height - 1 - height));

                for (int i = x + (partitionWidth * (count - 1)); i < width + (partitionWidth * (count - 1)) + x; i++)
                {
                    for (int j = y; j < height + y; j++)
                    {
                        if (j == y || j == height + y - 1)
                        {
                            _map[i, j] = '-';
                        }
                        else if (i == x + (partitionWidth * (count - 1)) || i == width + x - 1 + (partitionWidth * (count - 1)))
                        {
                            _map[i, j] = '|';
                        }
                        else
                        {
                            _map[i, j] = '.';
                        }

                    }
                }
                count++;
            }

        }

        public void DisplayLevel()
        {
            for (int j = 0; j < _height; j++)
            {
                Console.Write("\n");
                for (int i = 0; i < _width; i++)
                {
                    if (_map[i,j] == '*')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(_map[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else
                    {
                        Console.Write(_map[i, j]);
                    }
                    

                }
            }
        }

        public void AddVertices()
        {
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    if (_map[i, j] == ' ')
                    {
                        Vertex<char> tempV = new Vertex<char>(_map[i, j]);
                        (int, int) l = (i, j);
                        tempV.Location = l;
                        _vertexDictionary.Add((i, j), tempV);
                    }
                }
            }
        }

        public void AddNeighborsAndEdges()
        {
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    (int, int) temp = (i, j);

                    Console.Write(_map[i, j]);

                    if (_map[i, j] == ' ')
                    {
                        //left
                        if (i - 1 >= 0 && _map[i - 1, j] == ' ')
                        {
                            (int, int) left = (i - 1, j);
                            WeightedEdge<char> e = new WeightedEdge<char>(_vertexDictionary[temp], _vertexDictionary[left], 1);
                            _edges.Add(e);
                            _vertexDictionary[temp].AddEdge(e);


                        }
                        //right
                        if (i + 1 < _width && _map[i + 1, j] == ' ')
                        {
                            (int, int) right = (i + 1, j);
                            WeightedEdge<char> e = new WeightedEdge<char>(_vertexDictionary[temp], _vertexDictionary[right], 1);
                            _edges.Add(e);
                            _vertexDictionary[temp].AddEdge(e);
                        }
                        //down
                        if (j - 1 >= 0 && _map[i, j - 1] == ' ')
                        {
                            (int, int) down = (i, j - 1);
                            WeightedEdge<char> e = new WeightedEdge<char>(_vertexDictionary[temp], _vertexDictionary[down], 1);
                            _edges.Add(e);
                            _vertexDictionary[temp].AddEdge(e);
                        }
                        //up
                        if (j + 1 < _height && _map[i, j + 1] == ' ')
                        {
                            (int, int) up = (i, j + 1);
                            WeightedEdge<char> e = new WeightedEdge<char>(_vertexDictionary[temp], _vertexDictionary[up], 1);
                            _edges.Add(e);
                            _vertexDictionary[temp].AddEdge(e);
                        }

                        _vertices.Add(_vertexDictionary[temp]);

                    }
                }
                Console.WriteLine();
            }
        }

        public void FindPath()
        {
            string algorithm = "AStar";

            WeightedGraph<char> graph = new WeightedGraph<char>(_vertices, _edges);
            List<Vertex<char>> path = graph.Pathfinder(_vertexDictionary[_start], _vertexDictionary[_end], algorithm);

            foreach (Vertex<char> v in path)
            {
                (int, int) l = v.Location;
                _map[l.Item1, l.Item2] = '*';
            }
        }

    }
}
