namespace Pacman
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;
    using Pacman.Engines;
    using Pacman.Enumerations;
    using Pacman.GameObjects.MovableObjects;
    using Pacman.Interfaces;
    using Pacman.sound;
    using System;
    using System.Media;

    class MainProgram
    {
        static void Menu()
        {
            Console.SetCursorPosition(3, 5);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("******     *      ******  *       *     *     *     *");
            Console.SetCursorPosition(3, 6);
            Console.WriteLine("*     *    *     *      * **     **     *     **    *");
            Console.SetCursorPosition(3, 7);
            Console.WriteLine("*     *   * *    *      * * *   * *    * *    * *   *");
            Console.SetCursorPosition(3, 8);
            Console.WriteLine("*     *   * *    *        *  * *  *    * *    *  *  *");
            Console.SetCursorPosition(3, 9);
            Console.WriteLine("*     *  *   *   *        *   *   *   *   *   *   * *");
            Console.SetCursorPosition(3, 10);
            Console.WriteLine("******   *   *   *        *       *   *   *   *    **");
            Console.SetCursorPosition(3, 11);
            Console.WriteLine("*       *******  *        *       *  *******  *     *");
            Console.SetCursorPosition(3, 12);
            Console.WriteLine("*       *     *  *      * *       *  *     *  *     *");
            Console.SetCursorPosition(3, 13);
            Console.WriteLine("*      *       * *      * *       * *       * *     *");
            Console.SetCursorPosition(3, 14);
            Console.WriteLine("*      *       *  ******  *       * *       * *     *");
            Console.ResetColor();
            Console.SetCursorPosition(15, 18);
            Console.WriteLine("Press eny key to continue");
            ConsoleKeyInfo command = Console.ReadKey();
        }

        static void Main()
        {
            
            Map map = new Map(Constant.MapPath);
            Console.Title = "Pacman";
            Console.CursorVisible = false;
            Console.WindowHeight = map.AllRows + Constant.AdditionalRows;
            Console.WindowWidth = map.AllColls + Constant.AdditionalColumns;
            Console.BufferWidth = map.AllColls + Constant.AdditionalColumns;
            Console.BufferHeight = map.AllRows + Constant.AdditionalRows;

            MusicPlayer.StartMusic.Play();
            Menu();

            IUserInput keyboard = new KeyboardInput();
            IRenderer renderer = new ConsoleRenderer(map.AllRows, map.AllColls);

            GameEngine engine = new GameEngine(keyboard, renderer, map);

            Character pacman = new Character('@', new MatrixCoords(Constant.PacmanRowStartPosition, Constant.PacmanColStartPosition));
            for (int i = 60; i <= 90; i += 10)
            {
                Opponent opponent = new Opponent('$', new MatrixCoords(Constant.OpponentRowStartPosition, Constant.OpponentColStartPosition), i);
                engine.AddObject(opponent);
            }

            Action<Direction> pacmanMoves = pacman.SetWaitingDirection;

            pacmanMoves.Invoke(Direction.Up);

            keyboard.OnUpPressed += (sender, eventArgs) => { pacmanMoves.Invoke(Direction.Up); };
            keyboard.OnDownPressed += (sender, eventArgs) => { pacmanMoves.Invoke(Direction.Down); };
            keyboard.OnLeftPressed += (sender, eventArgs) => { pacmanMoves.Invoke(Direction.Left); };
            keyboard.OnRightPressed += (sender, eventArgs) => { pacmanMoves.Invoke(Direction.Right); };

            engine.AddObject(pacman);
            
            engine.Run();
        }
    }
}
