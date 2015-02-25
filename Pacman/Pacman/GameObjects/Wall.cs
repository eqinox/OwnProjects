namespace Pacman.GameObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;

    class Wall : GameObject
    {
        public Wall(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.team = Constant.WallTeam;
        }
    }
}
