using Pacman.ConsoleThings;
using Pacman.Constants;
using Pacman.GameObjects.MovableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
