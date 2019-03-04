using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraGrid
{
    class Maze
    {

        public class Grid { 
            public char [,] map ={ { '+', '+', '+' },
                                   { '+', '+', '+' },
                                   { '+', '+', '+' } };
        }

        public int _width;
        public int _height;
        public Stack<Vertex<Grid>> _path;

        public List<Vertex<Grid>> _vertices = new List<Vertex<Grid>>();
        public List<WeightedEdge<Grid>> _edges = new List<WeightedEdge<Grid>>();
        Dictionary<(int, int), Vertex<Grid>> _vertexDictionary = new Dictionary<(int, int), Vertex<Grid>>();
        //Dictionary<Vertex<Grid>, Vertex<Grid>> parentMap = new Dictionary<Vertex<Grid>, Vertex<Grid>>();

        public Grid[,] _map;
        public char[,] exportedMap;

        public static Random rnd = new Random();

        public Maze(int width, int height)
        {
            _width = width;
            _height = height;
            _path = new Stack<Vertex<Grid>>();
            _map = new Grid[width, height];
            exportedMap = new char[_width * 3, _height * 3];
        }

        public void InitializeMaze()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _map[i, j] = new Grid();
                }
            }
        }

        public void AddVertices()
        {
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    Vertex<Grid> tempV = new Vertex<Grid>(_map[i, j]);
                    (int, int) l = (i, j);
                    tempV.Location = l;
                    _vertexDictionary.Add((i, j), tempV);
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

                    //Console.Write(_map[i, j]);

                    //left
                    if (i - 1 >= 0)
                    {
                        (int, int) left = (i - 1, j);
                        WeightedEdge<Grid> e = new WeightedEdge<Grid>(_vertexDictionary[temp], _vertexDictionary[left], 1);
                        _edges.Add(e);
                        _vertexDictionary[temp].AddEdge(e);


                    }
                    //right
                    if (i + 1 < _width)
                    {
                        (int, int) right = (i + 1, j);
                        WeightedEdge<Grid> e = new WeightedEdge<Grid>(_vertexDictionary[temp], _vertexDictionary[right], 1);
                        _edges.Add(e);
                        _vertexDictionary[temp].AddEdge(e);
                    }
                    //down
                    if (j - 1 >= 0 )
                    {
                        (int, int) down = (i, j - 1);
                        WeightedEdge<Grid> e = new WeightedEdge<Grid>(_vertexDictionary[temp], _vertexDictionary[down], 1);
                        _edges.Add(e);
                        _vertexDictionary[temp].AddEdge(e);
                    }
                    //up
                    if (j + 1 < _height)
                    {
                        (int, int) up = (i, j + 1);
                        WeightedEdge<Grid> e = new WeightedEdge<Grid>(_vertexDictionary[temp], _vertexDictionary[up], 1);
                        _edges.Add(e);
                        _vertexDictionary[temp].AddEdge(e);
                    }

                    _vertices.Add(_vertexDictionary[temp]);
                }
                //Console.WriteLine();
            }
        }

        public void BuildMaze()
        {
            WeightedGraph<Grid> graph = new WeightedGraph<Grid>(_vertices, _edges);
            Vertex<Grid> start = _vertexDictionary[(0,0)];
            start.IsVisited = true;
            Vertex<Grid> current = start;
            _map[start.Location.Item1, start.Location.Item2].map[0, 0] = ' ';
            _map[start.Location.Item1, start.Location.Item2].map[1, 0] = ' ';
            _map[start.Location.Item1, start.Location.Item2].map[0, 1] = ' ';
            int count = 1;
            Traverse(count, current);
            //CarveOut();

        }

        public void Traverse(int count, Vertex<Grid> current)
        {
            while (count < _width * _height)
            {
                Stack<Vertex<Grid>> neighbors = new Stack<Vertex<Grid>>();
                foreach (Vertex<Grid> v in current.Neighbors)
                {
                    if (v.IsVisited == false)
                    {
                        neighbors.Push(v);
                    }
                }

                if (neighbors.Count != 0)
                {
                    Vertex<Grid>[] a = new Vertex<Grid>[neighbors.Count];
                    while (neighbors.Count != 0)
                    {
                        a[neighbors.Count - 1] = neighbors.Pop();
                    }

                    // choose a random unvisited neighbor of current
                    int random = rnd.Next(0, a.Length);
                    Vertex<Grid> nextNeighbor;
                    nextNeighbor = a[random];

                    // Push the current cell to the stack
                    _path.Push(current);

                    // connect path
                    //parentMap.Add(current, nextNeighbor);
                    CarveOut(nextNeighbor, current);
                    DisplayMaze();

                    current = nextNeighbor;
                    nextNeighbor.IsVisited = true;
                    count++;

                }
                else
                {
                    current = _path.Pop();
                }
                //Traverse(count, current);
            }

            //_map[current.Location.Item1, current.Location.Item2].map[]
        }

        public void CarveOut(Vertex<Grid> nextNeighbor, Vertex<Grid> current)
        {

            // up
            (int, int) l = current.Location;
            if (current.Location.Item2 > nextNeighbor.Location.Item2)
            {
                _map[l.Item1, l.Item2].map[1, 1] = ' ';
                _map[l.Item1, l.Item2].map[1, 0] = ' ';
                l = nextNeighbor.Location;
                _map[l.Item1, l.Item2].map[1, 1] = ' ';
                _map[l.Item1, l.Item2].map[1, 2] = ' ';
            }
            // down
            else if (current.Location.Item2 < nextNeighbor.Location.Item2)
            {
                _map[l.Item1, l.Item2].map[1, 1] = ' ';
                _map[l.Item1, l.Item2].map[1, 2] = ' ';
                l = nextNeighbor.Location;
                _map[l.Item1, l.Item2].map[1, 1] = ' ';
                _map[l.Item1, l.Item2].map[1, 0] = ' ';
            }
            // left
            else if (current.Location.Item1 > nextNeighbor.Location.Item1)
            {
                _map[l.Item1, l.Item2].map[1, 1] = ' ';
                _map[l.Item1, l.Item2].map[0, 1] = ' ';
                l = nextNeighbor.Location;
                _map[l.Item1, l.Item2].map[1, 1] = ' ';
                _map[l.Item1, l.Item2].map[2, 1] = ' ';
            }
            // right
            else if (current.Location.Item1 < nextNeighbor.Location.Item1)
            {
                _map[l.Item1, l.Item2].map[1, 1] = ' ';
                _map[l.Item1, l.Item2].map[2, 1] = ' ';
                l = nextNeighbor.Location;
                _map[l.Item1, l.Item2].map[1, 1] = ' ';
                _map[l.Item1, l.Item2].map[0, 1] = ' ';
            }

            //foreach (KeyValuePair<Vertex<Grid>, Vertex<Grid>> v in parentMap)
            //{
            //    // up
            //    (int, int) l = v.Key.Location;
            //    if (v.Key.Location.Item2 > v.Value.Location.Item2)
            //    {
            //        _map[l.Item1, l.Item2].map[1, 1] = ' ';
            //        _map[l.Item1, l.Item2].map[1, 0] = ' ';
            //        l = v.Value.Location;
            //        _map[l.Item1, l.Item2].map[1, 1] = ' ';
            //        _map[l.Item1, l.Item2].map[1, 2] = ' ';
            //    }
            //    // down
            //    else if (v.Key.Location.Item2 < v.Value.Location.Item2)
            //    {
            //        _map[l.Item1, l.Item2].map[1, 1] = ' ';
            //        _map[l.Item1, l.Item2].map[1, 2] = ' ';
            //        l = v.Value.Location;
            //        _map[l.Item1, l.Item2].map[1, 1] = ' ';
            //        _map[l.Item1, l.Item2].map[1, 0] = ' ';
            //    }
            //    // left
            //    else if (v.Key.Location.Item1 > v.Value.Location.Item1)
            //    {
            //        _map[l.Item1, l.Item2].map[1, 1] = ' ';
            //        _map[l.Item1, l.Item2].map[0, 1] = ' ';
            //        l = v.Value.Location;
            //        _map[l.Item1, l.Item2].map[1, 1] = ' ';
            //        _map[l.Item1, l.Item2].map[2, 1] = ' ';
            //    }
            //    // right
            //    else if (v.Key.Location.Item1 < v.Value.Location.Item1)
            //    {
            //        _map[l.Item1, l.Item2].map[1, 1] = ' ';
            //        _map[l.Item1, l.Item2].map[2, 1] = ' ';
            //        l = v.Value.Location;
            //        _map[l.Item1, l.Item2].map[1, 1] = ' ';
            //        _map[l.Item1, l.Item2].map[0, 1] = ' ';
            //    }

            //}
        }

        public void DisplayMaze()
        {
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    for (int h = 0; h < 3; h++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            Console.SetCursorPosition(i * 3 + 1 + k, j * 3 + h + 1);
                            exportedMap[i * 3 + k, j * 3 + h] = _map[i, j].map[k, h];
                            Console.Write(_map[i, j].map[k,h]);
                        }
                    }
                }
            }
        }


    }
}
