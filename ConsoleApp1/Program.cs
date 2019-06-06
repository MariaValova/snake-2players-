using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


class Program
{
    static int left = 0;
    static int right = 1;
    static int up = 2;
    static int down = 3;


    static int firstPlayerScore = 0;
    static int firstPlayerDirection = right;
    static int firstPlayerColumn = 0; // вертикаль
    static int firstPlayerRow = 0; // строка


    static int secondPlayerScore = 0;
    static int secondPlayerDirection = left;
    static int secondPlayerColumn = 40; // вертикаль
    static int secondPlayerRow = 5; // строка



    static bool[,] isUsed;


    static void Main(string[] args)
    {
        SetGameField();
        StartupScreen();

        isUsed = new bool[Console.WindowWidth, Console.WindowHeight];


        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                ChangePlayerDirection(key);
            }


            MovePlayers(); // движение


            bool firstPlayerLoses = DoesPlayerLose(firstPlayerRow, firstPlayerColumn);
            bool secondPlayerLoses = DoesPlayerLose(secondPlayerRow, secondPlayerColumn);


            if (firstPlayerLoses && secondPlayerLoses)
            {
                firstPlayerScore++;
                secondPlayerScore++;
                Console.WriteLine();
                Console.WriteLine("Game over");
                Console.WriteLine("Ничья!!!");
                Console.WriteLine("Текущий счет: {0} - {1}", firstPlayerScore, secondPlayerScore);
                ResetGame();
            }
            if (firstPlayerLoses)
            {
                secondPlayerScore++;
                Console.WriteLine();
                Console.WriteLine("Game over");
                Console.WriteLine("Игрок №2 победил!!!");
                Console.WriteLine("Текущий счет: {0} - {1}", firstPlayerScore, secondPlayerScore);
                ResetGame();
            }
            if (secondPlayerLoses)
            {
                firstPlayerScore++;
                Console.WriteLine();
                Console.WriteLine("Game over");
                Console.WriteLine("Игрок №1 победил!!!");
                Console.WriteLine("Текущий счет: {0} - {1}", firstPlayerScore, secondPlayerScore);
                ResetGame();
            }


            isUsed[firstPlayerColumn, firstPlayerRow] = true;
            isUsed[secondPlayerColumn, secondPlayerRow] = true;


            WriteOnPosition(firstPlayerColumn, firstPlayerRow, '*', ConsoleColor.Magenta);
            WriteOnPosition(secondPlayerColumn, secondPlayerRow, '*', ConsoleColor.White);


            Thread.Sleep(100);
        }
    }


    static void StartupScreen()
    {
        string heading = "Игра";
        Console.CursorLeft = Console.BufferWidth / 2 - heading.Length / 2;
        Console.WriteLine(heading);


        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Управление игрока №1:\n");
        Console.WriteLine("W - ВВЕРХ");
        Console.WriteLine("A - ВЛЕВО");
        Console.WriteLine("S - ВНИЗ");
        Console.WriteLine("D - ВПРАВО");

        string longestString = "Управление игрока №2:";
        int cursorLeft = Console.BufferWidth - longestString.Length;

        Console.CursorTop = 1;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.CursorLeft = cursorLeft;
        Console.WriteLine("Управление игрока №2:");
        Console.CursorLeft = cursorLeft;
        Console.WriteLine("Up Arrow - ВВЕРХ");
        Console.CursorLeft = cursorLeft;
        Console.WriteLine("Left Arrow - ВЛЕВО");
        Console.CursorLeft = cursorLeft;
        Console.WriteLine("Down Arrow - ВНИЗ");
        Console.CursorLeft = cursorLeft;
        Console.WriteLine("Right Arrow - ВПРАВО");

        Console.ReadKey();
        Console.Clear(); // очищение экрана
    }
    static void ResetGame() //сброс игры
    {
        isUsed = new bool[Console.WindowWidth, Console.WindowHeight];
        SetGameField();
        firstPlayerDirection = right;
        secondPlayerDirection = left;
        Console.WriteLine("Нажмите любую клавишу, чтобы начать игру");
        Console.ReadKey();
        Console.Clear();
        MovePlayers();
    }


    static bool DoesPlayerLose(int row, int col) // игрок проигрывает
    {
        if (row < 0)
        {
            return true;
        }
        if (col < 0)
        {
            return true;
        }
        if (row >= Console.WindowHeight)
        {
            return true;
        }
        if (col >= Console.WindowWidth)
        {
            return true;
        }


        if (isUsed[col, row])
        {
            return true;
        }


        return false;
    }


    static void SetGameField() // игровое поле
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WindowHeight = 30;
        Console.BufferHeight = 30;


        Console.WindowWidth = 100;
        Console.BufferWidth = 100;



        firstPlayerColumn = 0;
        firstPlayerRow = Console.WindowHeight / 2;


        secondPlayerColumn = Console.WindowWidth - 1;
        secondPlayerRow = Console.WindowHeight / 2;
    }


    static void MovePlayers() // движение игроков
    {
        if (firstPlayerDirection == right)
        {
            firstPlayerColumn++;
        }
        if (firstPlayerDirection == left)
        {
            firstPlayerColumn--;
        }
        if (firstPlayerDirection == up)
        {
            firstPlayerRow--;
        }
        if (firstPlayerDirection == down)
        {
            firstPlayerRow++;
        }


        if (secondPlayerDirection == right)
        {
            secondPlayerColumn++;
        }
        if (secondPlayerDirection == left)
        {
            secondPlayerColumn--;
        }
        if (secondPlayerDirection == up)
        {
            secondPlayerRow--;
        }
        if (secondPlayerDirection == down)
        {
            secondPlayerRow++;
        }
    }


    static void WriteOnPosition(int x, int y, char ch, ConsoleColor color) //позиция
    {
        Console.ForegroundColor = color;
        Console.SetCursorPosition(x, y);
        Console.Write(ch);
    }


    static void ChangePlayerDirection(ConsoleKeyInfo key) // изменение направления
    {
        if (key.Key == ConsoleKey.W && firstPlayerDirection != down)
        {
            firstPlayerDirection = up;
        }
        if (key.Key == ConsoleKey.A && firstPlayerDirection != right)
        {
            firstPlayerDirection = left;
        }
        if (key.Key == ConsoleKey.D && firstPlayerDirection != left)
        {
            firstPlayerDirection = right;
        }
        if (key.Key == ConsoleKey.S && firstPlayerDirection != up)
        {
            firstPlayerDirection = down;
        }


        if (key.Key == ConsoleKey.UpArrow && secondPlayerDirection != down)
        {
            secondPlayerDirection = up;
        }
        if (key.Key == ConsoleKey.LeftArrow && secondPlayerDirection != right)
        {
            secondPlayerDirection = left;
        }
        if (key.Key == ConsoleKey.RightArrow && secondPlayerDirection != left)
        {
            secondPlayerDirection = right;
        }
        if (key.Key == ConsoleKey.DownArrow && secondPlayerDirection != up)
        {
            secondPlayerDirection = down;
        }
    }
}

