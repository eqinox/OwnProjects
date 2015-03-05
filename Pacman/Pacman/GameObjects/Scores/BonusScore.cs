namespace Pacman.GameObjects.Scores
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;

    class BonusScore : Score
    {
        public BonusScore(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.value = Constant.BonusScoreValue;
        }
    }
}
