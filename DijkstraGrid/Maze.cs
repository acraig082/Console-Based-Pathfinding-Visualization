using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraGrid
{
    class Maze
    {
        public int _width;
        public int _height;
        public Stack<(int, int)> _visited;

        public char[,] _map;

        Random rnd = new Random();

        public Maze(int width, int height)
        {
            _width = width;
            _height = height;
            _visited = new Stack<(int, int)>();
            _map = new char[width, height];
        }

        public void InitializeMaze()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _map[i, j] = ' ';
                }
            }
        }
    }
}
