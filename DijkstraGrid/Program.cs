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

            Console.WriteLine(" ___  ___ __  __  ____ ___  ___   ___   __  __ __  ___ ");
            Console.WriteLine(@" ||\\//|| ||\ || ||    ||\\//||  // \\  ||\ || || // \\");
            Console.WriteLine(@" || \/ || ||\\|| ||==  || \/ || ((   )) ||\\|| || ||=||");
            Console.WriteLine(@" ||    || || \|| ||___ ||    ||  \\_//  || \|| || || ||");
            Console.WriteLine("");
            Console.WriteLine("");

            char choice;
            int width;
            int height;
            int rooms;
            bool correctInput = false;

            do
            {
                Console.WriteLine("Welcome to my pathfinding program. Would you like the computer to solve a maze or level full of rooms?(m)aze or (l)evel:");
                choice = Console.ReadKey().KeyChar;
                if (choice == 'm' || choice == 'l')
                {
                    correctInput = true;
                }
            } while (!correctInput);

            if (choice == 'm')
            {
                do
                {
                    Console.WriteLine("\nHow wide would you like the maze to be?(4 - 30):");
                    width = Convert.ToInt32(Console.ReadLine());
                } while (width < 4 || width > 30);

                do
                {
                    Console.WriteLine("\nHow tall would you like the maze to be?(4 - 10):");
                    height = Convert.ToInt32(Console.ReadLine());
                } while (height < 4 || height > 10);

                rooms = 0;

                Console.Clear();

                Maze maze = new Maze(width, height);
                maze.InitializeMaze();
                maze.AddVertices();
                maze.AddNeighborsAndEdges();
                maze.BuildMaze();
                //maze.DisplayMaze();

                Level level = new Level(width *3, height*3, rooms, maze.exportedMap);
                //level.InitializeLevel();
                //level.AddRandomRooms();
                level.AddVertices();
                level.AddNeighborsAndEdges();
                level.FindPath();
                level.DisplayLevel();


                Console.ReadKey();

            }

            else if (choice == 'l')
            {
                do
                {
                    Console.WriteLine("\nHow wide would you like the level to be?(4 - 40):");
                    width = Convert.ToInt32(Console.ReadLine());
                } while (width < 4 || width > 40);

                do
                {
                    Console.WriteLine("\nHow tall would you like the level to be?(4 - 15):");
                    height = Convert.ToInt32(Console.ReadLine());
                } while (height < 4 || height > 15);

                do
                {
                    Console.WriteLine("\nHow many rooms would you like to be in the level? (1 - 7):");
                    rooms = Convert.ToInt32(Console.ReadLine());
                } while (rooms < 1 || rooms > 7);

                Console.Clear();

                Level level = new Level(width * 3, height * 3, rooms);
                level.InitializeLevel();
                level.AddRandomRooms();
                level.AddVertices();
                level.AddNeighborsAndEdges();
                level.FindPath();
                level.DisplayLevel();


                Console.ReadKey();
            }
            else
            {
                Console.ReadKey();
            }



        }

    }
}
