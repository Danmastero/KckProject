using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Snake
{
    class Program
    {
        private const int yCordTop = 10;
        private const int Width = 100;
        private const int Height = 50;
        private const int xCordTop = 5;

        static void Main(string[] args)
        {
            // some setup
            bool game = true;
            bool play = false;
            int index;

            string prop = "Select your destiny:";
            string[] options1 = { "Start game", "Quit game" };
            string[] options2 = { "Main menu", "Quit game" };
            string[] options3 = { "Continue", "End game" };

            int banerY = 15;
            int score;

            string[] snakeBaner = new[]
            {
                @"   oo_                         ",
                @"  /  _)-<             _        ",
                @"  \__ `.             | |       ",
                @"     `. | _ __   __ _| | _____ ",
                @"     _| || '_ \ / _` | |/ / _ \",
                @"  ,-'   || | | | (_| |   <  __/",
                @"(_..--'  |_| |_|\__,_|_|\_\___|"
            };

            string[] gameover = new[]
            {
              @"   ██████╗  █████╗ ███╗   ███╗███████╗",
              @"  ██╔════╝ ██╔══██╗████╗ ████║██╔════╝",
              @"  ██║  ███╗███████║██╔████╔██║█████╗  ",
              @"  ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ",
              @"  ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗",
              @"   ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝",
              @"                                      ",
              @"   ██████╗ ██╗   ██╗███████╗██████╗   ",
              @"  ██╔═══██╗██║   ██║██╔════╝██╔══██╗  ",
              @"  ██║   ██║██║   ██║█████╗  ██████╔╝  ",
              @"  ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗  ",
              @"  ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║  ",
              @"   ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝  "
            };

            
            Menu Main = new Menu(prop, options1, 31); 
            Menu End = new Menu(prop, options2, 31); 
            Menu Pause = new Menu("Choose what to do:", options3, 31); 

            while (game)
            {
                Console.SetWindowSize(Width, Height);
                Console.CursorVisible = false;
                score = 0;
                Snake player = new Snake();
                Console.Clear();
                MapBoarders();
                ShowTop(score);
                ShowBaner(snakeBaner, banerY);
                index = Main.MakeMenu();
                if (index == 0) { play = true; }
                else if (index == 1) { game = false; }
                Console.Clear();
                player.MakeSnack();
                player.MakeDrug();
                MapBoarders();

                while (play)
                {
                    Console.SetWindowSize(Width, Height);
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    ShowTop(score);

                    play = player.DrawGame();
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo press = Console.ReadKey();
                        switch (press.Key)
                        {
                            case (ConsoleKey.UpArrow):
                                if (player.directon != "DOWN") { player.directon = "UP"; }
                                break;
                            case (ConsoleKey.DownArrow):
                                if (player.directon != "UP") { player.directon = "DOWN"; }
                                break;
                            case (ConsoleKey.LeftArrow):
                                if (player.directon != "RIGHT") { player.directon = "LEFT"; }
                                break;
                            case (ConsoleKey.RightArrow):
                                if (player.directon != "LEFT") { player.directon = "RIGHT"; }
                                break;
                            case (ConsoleKey.Escape):
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                index = Pause.MakeMenu();
                                if (index == 0)
                                {
                                    Console.Clear();
                                    MapBoarders();
                                    break;
                                }
                                else if (index == 1) { play = false; }
                                break;
                        }
                    }
                    if (player.EatFood() == 1)
                    {
                        player.MakeSnack();
                        score += 5;
                        Console.Beep(800, 15);
                    }
                    if (player.EatFood() == 2)
                    {
                        player.MakeDrug();
                        score += 10;
                        Console.Beep(500, 15);
                    }
                }
                if (game)
                {
                    MapBoarders();
                    ShowBaner(gameover, banerY);
                    index = End.MakeMenu();
                    if (index == 0) { }
                    else if (index == 1) { game = false; }
                }
            }
        }

        static void ShowBaner(string[] baner, int Y)
        {
            foreach (string line in baner)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition((Console.WindowWidth / 2) - (line.Length / 2), Y);
                Console.WriteLine(line);
                Y++;
            }
        }

        static void MapBoarders()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(yCordTop, xCordTop);
            Console.Write("┌");
            Console.SetCursorPosition(90, xCordTop);
            Console.Write("┐");
            Console.SetCursorPosition(yCordTop, 45);
            Console.Write("└");
            Console.SetCursorPosition(90, 45);
            Console.Write("┘");

            for (int i = 11; i < 90; i++)
            {
                Console.SetCursorPosition(i, xCordTop);
                Console.Write("─");
                Console.SetCursorPosition(i, 45);
                Console.Write("─");
            }

            for (int i = 6; i < 45; i++)
            {
                Console.SetCursorPosition(yCordTop, i);
                Console.Write("│");
                Console.SetCursorPosition(90, i);
                Console.Write("│");
            }
        }

        static void ShowTop(int score)
        {
            Console.SetCursorPosition(18, 0);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("O");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("ooo");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" <- this is you, eat what you can, grow up, make a score");
            Console.SetCursorPosition(18, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("#");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" <- this is food, make you a bit longer, and slow your speed");
            Console.SetCursorPosition(18, 2);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" <- this is drug, make you really long, and speed you up");
            Console.SetCursorPosition(18, 3);
            Console.Write("Use arrorws to change direction of the snake, ESC to pause a game");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((Width / 2) - xCordTop, xCordTop);
            Console.Write("Score: {0}", score);
        }
    }
}