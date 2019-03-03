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
            public char [,] map ={ { '+', ' ', ' ', ' ', '+' },
                                   { ' ', ' ', ' ', ' ', ' ' },
                                   { '+', ' ', ' ', ' ', '+' } };
        }

        public char[,] up ={ { '+', '-', '-', '-', '+' },
                             { '|', ' ', ' ', ' ', '|' },
                             { '+', ' ', ' ', ' ', '+' } };

        public char[,] down ={ { '+', ' ', ' ', ' ', '+' },
                             { '|', ' ', ' ', ' ', '|' },
                             { '+', '-', '-', '-', '+' } };

        public char[,] left ={ { '+', '-', '-', '-', '+' },
                             { ' ', ' ', ' ', ' ', '|' },
                             { '+', '-', '-', '-', '+' } };

        public char[,] right ={ { '+', '-', '-', '-', '+' },
                             { '|', ' ', ' ', ' ', ' ' },
                             { '+', '-', '-', '-', '+' } };



        public int _width;
        public int _height;
        public Stack<Vertex<Grid>> _path;
        //Vertex<Grid> end;

         

        public List<Vertex<Grid>> _vertices = new List<Vertex<Grid>>();
        public List<WeightedEdge<Grid>> _edges = new List<WeightedEdge<Grid>>();
        Dictionary<(int, int), Vertex<Grid>> _vertexDictionary = new Dictionary<(int, int), Vertex<Grid>>();

        public Grid[,] _map;

        Random rnd = new Random();

        public Maze(int width, int height)
        {
            _width = width;
            _height = height;
            _path = new Stack<Vertex<Grid>>();
            _map = new Grid[width, height];
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
            Dictionary<Vertex<Grid>, Vertex<Grid>> parentMap = new Dictionary<Vertex<Grid>, Vertex<Grid>>();
            Vertex<Grid> start = _vertexDictionary[(0,0)];
            start.IsVisited = true;
            _path.Push(start);
            int count = 0;

            while (count < (_width * _height))
            {
                Vertex<Grid> current = _path.Pop();
                Stack<Vertex<Grid>> neighbors = new Stack<Vertex<Grid>>();
                foreach (Vertex<Grid> v in current.Neighbors)
                {
                    if (v.IsVisited == false)
                    {
                        neighbors.Push(v);
                    }
                }

                if (neighbors.Count == 0)
                {
                    break;
                }
                else
                {
                    // getting random neighbor
                    int random = rnd.Next(1, neighbors.Count);
                    Vertex<Grid> nextNeighbor;
                    for (int i = 0; i < random - 1; i++)
                    {
                        nextNeighbor = neighbors.Pop();
                    }
                    //end = neighbors.Peek();
                    nextNeighbor = neighbors.Pop();
                    nextNeighbor.IsVisited = true;

                    // connect path
                    parentMap.Add(current, nextNeighbor);
                    _path.Push(nextNeighbor);
                    count++;

                    while (neighbors.Count > 0)
                    {
                        neighbors.Pop();
                    }
                }

            }

            
            //List<Vertex<Grid>> path = graph.ReconstructPath(parentMap, start, end);

            foreach (KeyValuePair<Vertex<Grid>, Vertex<Grid>> v in parentMap)
            {
                // up
                (int, int) l = v.Key.Location;
                if (v.Key.Location.Item2 > v.Value.Location.Item2)
                {
                    _map[l.Item1, l.Item2].map = up;
                }
                // down
                else if(v.Key.Location.Item2 < v.Value.Location.Item2)
                {
                    _map[l.Item1, l.Item2].map = down;
                }
                // left
                else if (v.Key.Location.Item1 > v.Value.Location.Item1)
                {
                    _map[l.Item1, l.Item2].map = left;
                }
                // right
                else if (v.Key.Location.Item1 < v.Value.Location.Item1)
                {
                    _map[l.Item1, l.Item2].map = right;
                }

            }

        }

        public void DisplayMaze()
        {
            for (int j = 0; j < _height; j++)
            {
                Console.Write("\n");
                for (int i = 0; i < _width; i++)
                {
                    DisplayGrid(_map[i, j]);
                    Console.SetCursorPosition(i * 5, Console.CursorTop - j * 3);
                    
                }
            }
        }

        public void DisplayGrid(Grid g)
        {
            for (int h = 0; h < 3; h++)
            {
                Console.Write("\n");
                for (int k = 0; k < 5; k++)
                {
                    Console.Write(g.map[h,k]);
                }
            }
        }


    }
}
