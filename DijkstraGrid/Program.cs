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

            int width = 30;
            int height = 10;
            int rooms = 4;

            Maze maze = new Maze(width, height);
            maze.InitializeMaze();
            maze.AddVertices();
            maze.AddNeighborsAndEdges();
            maze.BuildMaze();
            maze.DisplayMaze();

            Level level = new Level(width *3, height*3, rooms, maze.exportedMap);
            //level.InitializeLevel();
            //level.AddRandomRooms();
            level.AddVertices();
            level.AddNeighborsAndEdges();
            level.FindPath();
            level.DisplayLevel();


            Console.ReadKey();

        }

    }
}
