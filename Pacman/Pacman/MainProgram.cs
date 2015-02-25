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
            Console.WindowHeight = map.AllRows + 1;
            Console.WindowWidth = map.AllColls + 1;
            Console.BufferWidth = map.AllColls + 1;
            Console.BufferHeight = map.AllRows + 1;


            IUserInterface keyboard = new KeyboardInterface();
            IRenderer renderer = new ConsoleRenderer(map.AllRows, map.AllColls);
            Map parser = new Map(Constant.MapPath);

            GameEngine engine = new GameEngine(keyboard, renderer, map);

            Character pacman = new Character('@', new MatrixCoords(1, 1));
            Opponent opponent1 = new Opponent('$', new MatrixCoords(5, 5));

            keyboard.OnUpPressed += (sender, eventArgs) => { pacman.Move(Direction.Up); };
            keyboard.OnDownPressed += (sender, eventArgs) => { pacman.Move(Direction.Down); };
            keyboard.OnLeftPressed += (sender, eventArgs) => { pacman.Move(Direction.Left); };
            keyboard.OnRightPressed += (sender, eventArgs) => { pacman.Move(Direction.Right); };

            engine.AddObject(pacman);
            engine.AddObject(opponent1);

            engine.Run();
        }
    }
}
