namespace Pacman.GameObjects.MovableObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;
    using Pacman.Enumerations;
    using Pacman.GameObjects.Scores;
    using Pacman.sound;

    class Character : MovableObject
    {
        public const int maxAttackModeTime = 50; // 50

        public int Scores { get; set; }
        public int Lives { get; set; }
        public int attackModeTimer { get; private set; }
        public bool AttackMode { get; set; } // Helps to attack opponents

        private bool alreadyTookLife;

        public Character(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.team = Team.Character;
            this.waitingDirection = Direction.Up;
            this.currentDirection = Direction.Up;
            this.AttackMode = false;
            this.Lives = Constant.CharacterDefaultLives;
        }

        public void TakeScore(Score score)
        {
            MusicPlayer.EatPill.Play();
            if (score is BonusScore)
            {
                this.AttackMode = true;
                this.attackModeTimer = maxAttackModeTime;
            }
            else if (score is BonusLifeScore)
            {
                this.Lives++;
                MusicPlayer.extralife.Play();
            }

            this.Scores += score.Value;
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
                MusicPlayer.extralife.Play();
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
            MusicPlayer.ghosteat.Play();
            opponent.Reset();
            this.Scores += opponent.Score;
        }
    }
}
