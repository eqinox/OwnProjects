namespace Pacman.GameObjects.MovableObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;
    using Pacman.GameObjects.Scores;

    class Character : MovableObject
    {
        private int scores;
        private bool attackMode;

        public Character(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.team = Constant.CharacterTeam;
            this.AttackMode = false;
        }
        /// <summary>
        /// Helps to attack opponents
        /// </summary>
        public bool AttackMode
        {
            get { return attackMode; }
            set { attackMode = value; }
        }

        public int Scores
        {
            get { return this.scores; }
        }

        public void TakeScore(Score score)
        {
            if (score.Value == Constant.BonusScoreValue)
            {
                this.AttackMode = true;
            }
            this.scores += score.Value;
            score.IsAlive = false;
        }
    }
}
