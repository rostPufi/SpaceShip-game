using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// TODO:  !!!Shooting, mySpaceShip = (O)
namespace JustSpaceShip
{
    // Add objects
    struct Object
    {
        public int x;
        public int y;
        public char c;
        public ConsoleColor color;

    }
    class SpaceShip
    {

        static void FixMyBuffer()
        {
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 45;
        }
        // TODO: Make a Game Intro
        static void PrintOnPosition(int x, int y, char c, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(c);
        }
        static void PrintInfoOnPosition(int x, int y, string str, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        static void Main()
        {
            // bufferfix
            FixMyBuffer();
            // play area
            int playfieldArea = Console.WindowWidth - Console.WindowWidth / 3;
            // speed count
            int speedCount = 0;
            // bullets
            int bullets = 3;
            // points
            int points = 0;
            // info Field
            int infoField = playfieldArea + 2;
            // LivesCount
            int liveCount = 8;
            // if hitted
            //bool hitted = false;
            // Add mySpaceShip
            Object mySpaceShip = new Object();
            mySpaceShip.x = playfieldArea / 2;
            mySpaceShip.y = Console.WindowHeight - 1;
            mySpaceShip.c = 'A';
            mySpaceShip.color = ConsoleColor.Yellow;
            // Bullets
            // Bullets as object
            Object MainBullets = new Object();
            MainBullets.x = mySpaceShip.x;
            // mainbullets.y
            //MainBullets.y = mySpaceShip.y;
            MainBullets.y = mySpaceShip.y;
            MainBullets.c = '^';
            MainBullets.color = mySpaceShip.color;
            //ListMainBullets.Add(MainBullets);
            // random generator
            Random randomGenerator = new Random();
            //AddRocks as LIst, still don't know what is this :)
            List<Object> ListMainRocks = new List<Object>();
            List<Object> ListMainPlayFieldLine = new List<Object>();
            List<Object> ListDoubleMainRocks = new List<Object>();
            while (true)
            {
                //Add gamefield infofield line
                Object playFieldLine = new Object();
                playFieldLine.x = playfieldArea;
                //playFieldLine.y = 0;
                playFieldLine.y = 0;
                playFieldLine.c = '|';
                playFieldLine.color = ConsoleColor.White;
                ListMainPlayFieldLine.Add(playFieldLine);
                //AddROcks as object
                Object MainRocks = new Object();
                MainRocks.x = randomGenerator.Next(0, playfieldArea - 1);
                MainRocks.y = 0;
                //MainRocks.c = 'Y';
                MainRocks.c = Convert.ToChar(Convert.ToInt32(randomGenerator.Next('\u0021', '\u002E')));
                MainRocks.color = ConsoleColor.White;
                ListMainRocks.Add(MainRocks);
                //Double add rocks as object
                Object DoubleMainRocks = new Object();
                DoubleMainRocks.x = MainRocks.x + 1;
                DoubleMainRocks.y = 0;
                DoubleMainRocks.c = Convert.ToChar(Convert.ToInt32(randomGenerator.Next('\u0021', '\u002E')));
                DoubleMainRocks.color = ConsoleColor.White;
                int DoubleRandom = randomGenerator.Next(0, 101);
                if (DoubleRandom < 70)
                {
                    ListDoubleMainRocks.Add(DoubleMainRocks);
                }
                // Move mySpaceShip
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                    if (mySpaceShip.x > 0)
                    {
                        if (pressedKey.Key == ConsoleKey.LeftArrow)
                        {
                            mySpaceShip.x--;
                            //MainBullets.x--;
                        }
                    }
                    if (mySpaceShip.x < playfieldArea - 1)
                    {
                        if (pressedKey.Key == ConsoleKey.RightArrow)
                        {
                            mySpaceShip.x++;
                            //MainBullets.x++;
                        }
                    }
                    if (MainBullets.y > Console.WindowHeight - Console.WindowHeight -1)
                    {
                        MainBullets.x = mySpaceShip.x;
                        if (pressedKey.Key == ConsoleKey.Spacebar)
                        {
                                MainBullets.y--;
                        }
                    }
                    if (MainBullets.y < Console.WindowHeight - Console.WindowHeight)
                    {
                        MainBullets.y = mySpaceShip.y;
                    }
                }
                // MoveRocks
                List<Object> newListMainRocks = new List<Object>();
                for (int i = 0; i < ListMainRocks.Count; i++)
                {
                    Object oldListMainRocks = ListMainRocks[i];
                    Object newObjectMainRocks = new Object();
                    newObjectMainRocks.x = oldListMainRocks.x;
                    newObjectMainRocks.y = oldListMainRocks.y + 1;
                    newObjectMainRocks.c = oldListMainRocks.c;
                    newObjectMainRocks.color = oldListMainRocks.color;
                    //ListMainRocks.Remove(oldListMainRocks);
                    if (newObjectMainRocks.x == mySpaceShip.x && newObjectMainRocks.y == mySpaceShip.y)
                    {
                        liveCount--;
                        //hitted = true;
                        // Speed
                        //PrintInfoOnPosition(infoField, 20, "hitted", ConsoleColor.Cyan);
                        speedCount = speedCount + 8;
                    }
                    if (liveCount + 1 <= 0)
                    {
                        PrintInfoOnPosition(playfieldArea / 2, 12, "The End \n", ConsoleColor.Cyan);
                        PrintInfoOnPosition(playfieldArea / 2, 14, "Your points are: " + points, ConsoleColor.Cyan);
                        PrintInfoOnPosition(playfieldArea / 2, 16, "Thank you, come again \n \n \n ", ConsoleColor.Cyan);
                        Console.WriteLine();
                        Environment.Exit(0);
                    }
                    if(newObjectMainRocks.y < Console.WindowHeight)
                    {
                        points += 1;
                        int pointsCount = 1000;
                        newListMainRocks.Add(newObjectMainRocks);
                        if (points == pointsCount)
                        {
                            speedCount = speedCount + 50;
                        }
                    }
                }
                ListMainRocks = newListMainRocks;
                // Add new List and For() for PlayField lIne
                List<Object> newListMainPlayFieldLine = new List<Object>();
                for (int j = 0; j < ListMainPlayFieldLine.Count; j++)
                {
                    Object oldListMaintPlayFieldLine = ListMainPlayFieldLine[j];
                    Object newObjectPlayFieldLine = new Object();
                    newObjectPlayFieldLine.x = oldListMaintPlayFieldLine.x;
                    newObjectPlayFieldLine.y = oldListMaintPlayFieldLine.y + 1;
                    newObjectPlayFieldLine.c = oldListMaintPlayFieldLine.c;
                    newObjectPlayFieldLine.color = oldListMaintPlayFieldLine.color;
                    if (newObjectPlayFieldLine.y < Console.WindowHeight)
                    {
                        newListMainPlayFieldLine.Add(newObjectPlayFieldLine);
                    }

                }
                ListMainPlayFieldLine = newListMainPlayFieldLine;
                // Double rocks
                List<Object> newListDoubleMainRocks = new List<Object>();
                for (int h = 0; h < ListDoubleMainRocks.Count; h++)
                {
                    Object oldListDoubleMainRocks = ListDoubleMainRocks[h];
                    Object newObjectDoubleMainRocks = new Object();
                    newObjectDoubleMainRocks.x = oldListDoubleMainRocks.x;
                    newObjectDoubleMainRocks.y = oldListDoubleMainRocks.y +1;
                    newObjectDoubleMainRocks.c = oldListDoubleMainRocks.c;
                    newObjectDoubleMainRocks.color = oldListDoubleMainRocks.color;
                    if (newObjectDoubleMainRocks.x == mySpaceShip.x && newObjectDoubleMainRocks.y == mySpaceShip.y)
                    {
                        liveCount--;
                        //hitted = true;
                        // Speed
                        speedCount = speedCount + 8;
                    }
                    if (newObjectDoubleMainRocks.y < Console.WindowHeight)
                    {
                        
                        //if (DoubleRandom < 70)
                        //{
                        newListDoubleMainRocks.Add(newObjectDoubleMainRocks);
                        //}
                    }
                }
                    ListDoubleMainRocks = newListDoubleMainRocks;
                //Bullets
               
                //Clear
                Console.Clear();
                // check if hitted


                //ship
                PrintOnPosition(MainBullets.x, MainBullets.y, MainBullets.c, MainBullets.color);
                PrintOnPosition(mySpaceShip.x, mySpaceShip.y, mySpaceShip.c, mySpaceShip.color);
                //rocks
                foreach (Object newObjectMainRocks in ListMainRocks)
                {
                    PrintOnPosition(newObjectMainRocks.x, newObjectMainRocks.y, newObjectMainRocks.c, newObjectMainRocks.color);
                }
                //infoLine
                foreach (Object newObjectPlayFieldLine in ListMainPlayFieldLine)
                {
                    PrintOnPosition(newObjectPlayFieldLine.x, newObjectPlayFieldLine.y, newObjectPlayFieldLine.c, newObjectPlayFieldLine.color);
                }
                // double rocks
                foreach (Object newObjectDoubleMainRocks in ListDoubleMainRocks)
                {
                    PrintOnPosition(newObjectDoubleMainRocks.x, newObjectDoubleMainRocks.y, newObjectDoubleMainRocks.c, newObjectDoubleMainRocks.color);
                }
                // info
                PrintInfoOnPosition(infoField, 2, "Lives: " + liveCount, ConsoleColor.Magenta);
                PrintInfoOnPosition(infoField, 4, "Points: " + points, ConsoleColor.Magenta);
                int speedCountInfo = speedCount + 100;
                PrintInfoOnPosition(infoField, 6, "Speed: " + speedCountInfo, ConsoleColor.Magenta);
                // slow down program
                Thread.Sleep(150 - (speedCount + 10));
            }
        }
    }
}
