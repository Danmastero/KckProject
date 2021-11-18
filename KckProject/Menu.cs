using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Menu
    {
        private string _prop;
        private string[] _options;
        private int _y;


        public Menu(string prop, string[] options, int y)
        {
            _prop = prop;
            _options = options;
            _y = y;
        }
        public int MakeMenu()
        {
            int index = 0;
            int posY;

            while (true)
            {
                posY = _y;
                Console.SetWindowSize(100, 50);
                Console.CursorVisible = false;
                Console.SetCursorPosition((Console.WindowWidth / 2) - (_prop.Length / 2), posY);
                Console.WriteLine(_prop);
                for (int i = 0; i < _options.Length; i++)
                {
                    posY++;
                    if (i == index)
                    {
                        Console.SetCursorPosition((Console.WindowWidth / 2) - (_prop.Length / 2), posY);
                        Console.WriteLine("> {0}", _options[i]);
                    }
                    else
                    {
                        Console.SetCursorPosition((Console.WindowWidth / 2) - (_prop.Length / 2), posY);
                        Console.WriteLine("  {0}", _options[i]);
                    }
                }
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo press = Console.ReadKey();
                    switch (press.Key)
                    {
                        case (ConsoleKey.UpArrow):
                            if (index > 0) { index--; }
                            break;
                        case (ConsoleKey.DownArrow):
                            if (index < _options.Length - 1) { index++; }
                            break;
                        case (ConsoleKey.Enter):
                            return index;
                    }
                }
            }

        }
    }
}