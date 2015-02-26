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
    using System.Threading;

    class MainProgram
    {
        static void Main()
        {
            Map map = new Map(Constant.MapPath);
            Console.Title = "Pacman";
            Console.WindowHeight = map.AllRows + Constant.AdditionalRows;
            Console.WindowWidth = map.AllColls + Constant.AdditionalColumns;
            Console.BufferWidth = map.AllColls + Constant.AdditionalColumns;
            Console.BufferHeight = map.AllRows + Constant.AdditionalRows;

            IUserInterface keyboard = new KeyboardInterface();
            IRenderer renderer = new ConsoleRenderer(map.AllRows, map.AllColls);
            Map parser = new Map(Constant.MapPath);

            GameEngine engine = new GameEngine(keyboard, renderer, map);

            Character pacman = new Character('@', new MatrixCoords(1, 2));
            Opponent opponent1 = new Opponent('$', new MatrixCoords(5, 5));

            keyboard.OnUpPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Up; };
            keyboard.OnDownPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Down; };
            keyboard.OnLeftPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Left; };
            keyboard.OnRightPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Right; };

            engine.AddObject(pacman);
            engine.AddObject(opponent1);

            engine.Run();
        }
    }
}
