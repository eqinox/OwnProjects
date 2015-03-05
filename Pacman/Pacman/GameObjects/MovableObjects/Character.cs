namespace Pacman.GameObjects.MovableObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;
    using Pacman.GameObjects.Scores;
    using System.Media;

    class Character : MovableObject
    {
        private int scores;
        private int lives;
        private int attackModeTimer = 0;
        private int maxAttackModeTime = 50;
        private bool attackMode;
        private bool alreadyTookLife;
        private SoundPlayer EatPill = new SoundPlayer(Constant.EatPillPath);
        private SoundPlayer extralife = new SoundPlayer(Constant.ExtraLifePath);
        private SoundPlayer ghosteat = new SoundPlayer(Constant.GhostEatPath);

        public Character(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.team = Constant.CharacterTeam;
            this.AttackMode = false;
            this.Lives = Constant.CharacterDefaultLives;
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
            set { this.scores = value; }
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public void TakeScore(Score score)
        {
            EatPill.Play();
            if (score is BonusScore)
            {
                this.AttackMode = true;
                this.attackModeTimer = this.maxAttackModeTime;
            }
            else if (score is BonusLifeScore)
            {
                this.Lives++;
                extralife.Play();
            }

            this.scores += score.Value;
            score.IsAlive = false;
        }

        public override void Update()
        {
            if (this.attackModeTimer > 0)
            {
                this.attackModeTimer--;
            }
            else
            {
                this.AttackMode = false;
            }

            if (this.Scores > Constant.EnoughScoreForeBonusLife && alreadyTookLife == false)
            {
                this.Lives++;
                extralife.Play();
                alreadyTookLife = true;
            }

            base.Update();
        }

        public override void Reset()
        {
            this.Position = new MatrixCoords(Constant.PacmanRowStartPosition, Constant.PacmanColStartPosition);
            base.Reset();
        }

        public void ResetEverything()
        {
            this.Reset();
            this.Lives = Constant.CharacterDefaultLives;
            this.Scores = 0;
        }

        public void EatOpponent(Opponent opponent)
        {
            ghosteat.Play();
            opponent.IsAlive = false;
            this.Scores += opponent.Score;
        }
    }
}
