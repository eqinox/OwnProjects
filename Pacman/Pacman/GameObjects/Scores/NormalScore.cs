namespace Pacman.GameObjects.Scores
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;

    class NormalScore : Score
    {
        public NormalScore(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.team = Constant.ScoreTeam;
            base.value = Constant.NormalScoreValue;
        }
    }
}
