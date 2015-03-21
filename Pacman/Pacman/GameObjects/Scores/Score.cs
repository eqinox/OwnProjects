namespace Pacman.GameObjects.Scores
{
    using Pacman.ConsoleThings;
    using Pacman.Enumerations;

    abstract class Score : GameObject
    {
        protected int value;

        protected Score(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.team = Team.Score;
        }

        public int Value
        {
            get
            {
                return this.value;
            }
        }

        public override string ToString()
        {
            return this.IsAlive.ToString();
        }
    }
}
