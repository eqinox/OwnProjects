namespace Pacman
{
    using Pacman.ConsoleThings;
    using Pacman.Engines;
    using Pacman.Enumerations;
    using Pacman.GameObjects;
    using Pacman.Interfaces;

    class MainProgram
    {
        static void Main()
        {
            IUserInterface keyboard = new KeyboardInterface();
            IRenderer renderer = new ConsoleRenderer(20, 20);

            GameEngine engine = new GameEngine(keyboard, renderer);

            Character pacman = new Character('@', new MatrixCoords(1, 1));


            keyboard.OnUpPressed += (sender, eventArgs) => { pacman.Move(Direction.Up); };
            keyboard.OnDownPressed += (sender, eventArgs) => { pacman.Move(Direction.Down); };
            keyboard.OnLeftPressed += (sender, eventArgs) => { pacman.Move(Direction.Left); };
            keyboard.OnRightPressed += (sender, eventArgs) => { pacman.Move(Direction.Right); };

            engine.AddObject(pacman);


            engine.Run();
        }
    }
}
