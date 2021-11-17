using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Snake
{
    class snake
    {
        public int lenght;
        public string directon;
        int speed = 100;
        private string body = "o";
        private string head = "O";
        readonly Random number = new Random();

        cords headpos = new cords(50, 20);  // where snake start
        cords tailpos = new cords(0, 1);

        food snack = new food(); // <- make a meal for a snake
        cords foodpos = new cords(0, 0);

        food drug = new food();
        cords drugpos = new cords(0, 0);

        readonly int[,] snakeBody = new int[500, 2];  // game map should be max 500 fields

        public snake()
        {
            lenght = 10;
            directon = "RIGHT";
        }

        public bool DrawGame()
        {
            Console.CursorVisible = false;

            // get old position of head
            snakeBody[lenght - 1, tailpos.x] = headpos.x;
            snakeBody[lenght - 1, tailpos.y] = headpos.y;

            // refresh position of the whole body
            for (int i = 2; i < lenght; i++)
            {
                snakeBody[i - 1, tailpos.x] = snakeBody[i, tailpos.x];
                snakeBody[i - 1, tailpos.y] = snakeBody[i, tailpos.y];
                if ((snakeBody[i - 2, tailpos.x] == headpos.x) && (snakeBody[i - 2, tailpos.y] == headpos.y)) { return false; }
            }

            // check if snake is still on the map
            if ((headpos.x > 89) || (headpos.x < 11) || (headpos.y > 44) || headpos.y < 6) { return false; }

            // drawFood
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(foodpos.x, foodpos.y);
            Console.Write(snack.foodbody1);

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(drugpos.x, drugpos.y);
            Console.Write(drug.foodbody2);

            // check if directon is change
            switch (directon)
            {
                case ("UP"):
                    headpos.y--;
                    break;
                case ("DOWN"):
                    headpos.y++;
                    break;
                case ("LEFT"):
                    headpos.x--;
                    break;
                case ("RIGHT"):
                    headpos.x++;
                    break;
            }

            // draw a snake
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(headpos.x, headpos.y);
            Console.Write(head);

            for (int i = 1; i < lenght; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(snakeBody[lenght - i, tailpos.x], snakeBody[lenght - i, tailpos.y]);
                Console.Write(body);
            }
            Thread.Sleep(speed);

            // and remove it form a screen
            /*Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(headpos.x, headpos.y);
            Console.Write(head);
            for (int i = 1; i < lenght; i++)
            {
                Console.SetCursorPosition(snakeBody[lenght - i, tailpos.x], snakeBody[lenght - i, tailpos.y]);
                Console.Write(body);
            }
            that code up removes the whole body (longer snake start to slowdown, lets remove only last part, like below
            */
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(snakeBody[0, tailpos.x], snakeBody[0, tailpos.y]);
            Console.Write(body);
            Console.SetCursorPosition(snakeBody[1, tailpos.x], snakeBody[1, tailpos.y]);
            Console.Write(body);
            Console.SetCursorPosition(0, 0);
            Console.Write(body);

            return true;
        }

        public int EatFood()
        {
            if ((headpos.x == foodpos.x) && (headpos.y == foodpos.y))
            {
                lenght++;
                speed += 5;
                return 1;
            }
            else if ((headpos.x == drugpos.x) && (headpos.y == drugpos.y))
            {
                lenght += 5;
                if (speed > 20) { speed -= 10; }
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public void MakeSnack()
        {
            foodpos.x = number.Next(11, 89);
            foodpos.y = number.Next(6, 44);
        }

        public void MakeDrug()
        {
            drugpos.x = number.Next(11, 89);
            drugpos.y = number.Next(6, 44);
        }
    }
}