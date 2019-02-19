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

        public char[,] _map;

        Random rnd = new Random();

        public Level(int width, int height, int rooms)
        {
            _width = width;
            _height = height;
            _rooms = rooms;

            _map = new char[width, height];

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _map[i, j] = ' ';
                }
            }
            
            addRandomRooms();
        }

        private void addRandomRooms()
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

        public void displayLevel()
        {
            for (int j = 0; j < _height; j++)
            {
                Console.Write("\n");
                for (int i = 0; i < _width; i++)
                {
                    Console.Write(_map[i,j]);
                }
            }
        }


    }
}
