namespace Pacman.GameObjects.MovableObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;

    class Opponent : MovableObject
    {
        public Opponent(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.team = Constant.OpponentTeam;
        }
    }
}
