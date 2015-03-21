using Pacman.ConsoleThings;
using Pacman.Constants;
using Pacman.GameObjects.MovableObjects;
namespace Pacman.GameObjects.Scores
{
    class BonusLifeScore : Score
    {
        public BonusLifeScore(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.value = Constant.SuperBonusScoreValue;
        }
    }
}
