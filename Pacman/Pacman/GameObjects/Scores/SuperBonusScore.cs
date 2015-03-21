namespace Pacman.GameObjects.Scores
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;

    class SuperBonusScore : Score
    {
        public SuperBonusScore(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.value = Constant.SuperBonusScoreValue;
        }
    }
}
