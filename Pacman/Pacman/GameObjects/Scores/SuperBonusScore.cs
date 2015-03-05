using Pacman.ConsoleThings;
using Pacman.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.GameObjects.Scores
{
    class SuperBonusScore : Score
    {
        public SuperBonusScore(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.value = Constant.SuperBonusScoreValue;
        }
    }
}
