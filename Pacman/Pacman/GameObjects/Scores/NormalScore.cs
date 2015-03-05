namespace Pacman.GameObjects.Scores
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;

    class NormalScore : Score
    {
        public NormalScore(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.value = Constant.NormalScoreValue;
        }
    }
}
