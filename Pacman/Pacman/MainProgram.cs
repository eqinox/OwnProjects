namespace Pacman
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;
    using Pacman.Engines;
    using Pacman.Enumerations;
    using Pacman.GameObjects;
    using Pacman.GameObjects.MovableObjects;
    using Pacman.Interfaces;
    using System;
    using System.Media;
    using System.Threading;

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
            Console.WriteLine("Press Enter to continue");
            string command = Console.ReadLine();
        }

        static void Main()
        {
            SoundPlayer StartMusic = new SoundPlayer(Constant.StartMusicPath);
            Map map = new Map(Constant.MapPath);
            Console.Title = "Pacman";
            Console.CursorVisible = false;
            Console.WindowHeight = map.AllRows + Constant.AdditionalRows;
            Console.WindowWidth = map.AllColls + Constant.AdditionalColumns;
            Console.BufferWidth = map.AllColls + Constant.AdditionalColumns;
            Console.BufferHeight = map.AllRows + Constant.AdditionalRows;

            StartMusic.Play();
            Menu();

            IUserInterface keyboard = new KeyboardInterface();
            IRenderer renderer = new ConsoleRenderer(map.AllRows, map.AllColls);
            Map parser = new Map(Constant.MapPath);

            GameEngine engine = new GameEngine(keyboard, renderer, map);

            Character pacman = new Character('@', new MatrixCoords(Constant.PacmanRowStartPosition, Constant.PacmanColStartPosition));
            for (int i = 1; i <= 4; i++)
            {
                Opponent opponent = new Opponent('$', new MatrixCoords(Constant.OpponentRowStartPosition, Constant.OpponentColStartPosition), i);
                engine.AddObject(opponent);
            }

            keyboard.OnUpPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Up; };
            keyboard.OnDownPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Down; };
            keyboard.OnLeftPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Left; };
            keyboard.OnRightPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Right; };

            engine.AddObject(pacman);
            

            engine.Run();
        }
    }
}
