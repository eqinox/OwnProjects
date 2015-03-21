namespace Pacman.GameObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Enumerations;

    class Path : GameObject
    {
        public Path(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.team = Team.Path;
        }
    }
}
