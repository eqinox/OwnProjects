namespace Pacman.GameObjects.Scores
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;

    abstract class Score : GameObject
    {
        protected int value;

        public Score(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.team = Constant.ScoreTeam;
        }

        public int Value
        {
            get
            {
                return this.value;
            }
        }
    }
}
