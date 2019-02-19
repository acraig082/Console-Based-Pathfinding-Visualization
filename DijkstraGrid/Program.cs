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
            level.displayLevel();

            
            


            Console.Read();
        }
    }
}
