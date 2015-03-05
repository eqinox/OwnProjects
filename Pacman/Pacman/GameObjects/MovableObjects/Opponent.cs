namespace Pacman.GameObjects.MovableObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;
    using Pacman.Enumerations;
    using System;

    class Opponent : MovableObject
    {
        private const int SmartestEnemy = 4;

        public bool StartFollow { get; set; }
        public int Score { get; set; }

        private MatrixCoords oldPosition;
        private int counterForHardStuck = 1;
        private int counterForSlowMotionMove = 1;
        private int levelOfSmart;
        private bool opponentHardStuck;
        private bool opponentStuck;
        private bool startPosition;
        private bool slowMotionMove;

        public Opponent(char symbol, MatrixCoords position, int levelOfSmart)
            :base(symbol, position)
        {
            base.team = Constant.OpponentTeam;
            this.oldPosition = new MatrixCoords(0, 0);
            this.LevelOfSmart = levelOfSmart;
            this.startPosition = true;
            this.StartFollow = false;
            this.Score = Constant.OpponentScore;
        }

        public int LevelOfSmart
        {
            get 
            {
                return 5 - this.levelOfSmart; 
            }
            set 
            {
                if (value > SmartestEnemy)
                {
                    value = SmartestEnemy;
                }
                this.levelOfSmart = value; 
            }
        }

        public void FollowCharacter(Character pacman)
        {
            this.slowMotionMove = false;
            if (this.StartFollow)
            {
                if (this.startPosition)
                {
                    this.waitingDirection = Direction.Up;
                    this.currentDirection = Direction.Up;
                    this.Move(Direction.Up);
                    this.Move(Direction.Up);
                    this.Move(Direction.Up);
                    this.startPosition = false;
                }
                else
                {
                    if (this.opponentStuck)
                    {
                        if (this.opponentHardStuck)
                        {
                            FollowWhenHardStuck(pacman);
                        }
                        else
                        {
                            FollowWhenStuck(pacman);
                        }
                    }
                    else
                    {
                        FollowByNormalDistanceDifference(pacman);
                    }
                }
            }
        }

        private void FollowWhenHardStuck(Character pacman)
        {
            if (this.currentDirection == Direction.Up)
            {
                if (this.waitingDirection == Direction.Left)
                {
                    this.waitingDirection = Direction.Right;
                }
                else
                {
                    this.waitingDirection = Direction.Left;
                }
            }
            if (this.currentDirection == Direction.Down)
            {
                if (this.waitingDirection == Direction.Left)
                {
                    this.waitingDirection = Direction.Right;
                }
                else
                {
                    this.waitingDirection = Direction.Left;
                }
            }
            if (this.currentDirection == Direction.Left)
            {
                if (this.waitingDirection == Direction.Up)
                {
                    this.waitingDirection = Direction.Down;
                }
                else
                {
                    this.waitingDirection = Direction.Up;
                }
            }
            if (this.currentDirection == Direction.Right)
            {
                if (this.waitingDirection == Direction.Up)
                {
                    this.waitingDirection = Direction.Down;
                }
                else
                {
                    this.waitingDirection = Direction.Up;
                }
            }
        }

        private void FollowWhenStuck(Character pacman)
        {
            Random randNum = new Random();

            bool correctAI = randNum.Next(0, this.LevelOfSmart) == 0 ? true : false;

            if (this.currentDirection == Direction.Up || this.currentDirection == Direction.Down)
            {
                if (this.Position.Col > pacman.Position.Col && correctAI) // if opponent is right and pacman left
                {
                    this.waitingDirection = Direction.Left;
                }
                else
                {
                    this.waitingDirection = Direction.Right;
                }
            }
            else if (this.currentDirection == Direction.Left || this.currentDirection == Direction.Right)
            {
                if (this.Position.Row > pacman.Position.Row && correctAI) // if opponent is down and pacman up
                {
                    this.waitingDirection = Direction.Up;
                }
                else
                {
                    this.waitingDirection = Direction.Down;
                }
            }
        }

        private void FollowByNormalDistanceDifference(Character pacman)
        {
            Random randNum = new Random();

            bool correctAI = randNum.Next(0, this.LevelOfSmart) == 0 ? true : false;

            if (this.Position.Row <= pacman.Position.Row) // if opponent is up and pacman down
            {
                int differenceBetweenRows = pacman.Position.Row - this.Position.Row;

                if (this.Position.Col <= pacman.Position.Col) // if opponent is left and pacman right
                {
                    int differenceBetweenCols = pacman.Position.Col - this.Position.Col;

                    if (differenceBetweenRows >= differenceBetweenCols && correctAI) // take longer way
                    {
                        if (this.currentDirection != Direction.Up)
                        {
                            this.waitingDirection = Direction.Down;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Left)
                        {
                            this.waitingDirection = Direction.Right;
                        }
                    }
                }
                else // if opponent is right and pacman left
                {
                    int differenceBetweenCols = this.Position.Col - pacman.Position.Col;

                    if (differenceBetweenRows >= differenceBetweenCols && correctAI) // take longer way
                    {
                        if (this.currentDirection != Direction.Up)
                        {
                            this.waitingDirection = Direction.Down;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Right)
                        {
                            this.waitingDirection = Direction.Left;
                        }
                    }
                }
            }
            else // if opponent is down and pacman up
            {
                int differenceBetweenRows = this.Position.Row - pacman.Position.Row;

                if (this.Position.Col <= pacman.Position.Col) // if opponent is left and pacman right
                {
                    int differenceBetweenCols = pacman.Position.Col - this.Position.Col;

                    if (differenceBetweenRows >= differenceBetweenCols && correctAI) // take longer way
                    {
                        if (this.currentDirection != Direction.Down)
                        {
                            this.waitingDirection = Direction.Up;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Left)
                        {
                            this.waitingDirection = Direction.Right;
                        }
                    }
                }
                else // if opponent is right and pacman left
                {
                    int differenceBetweenCols = this.Position.Col - pacman.Position.Col;

                    if (differenceBetweenRows >= differenceBetweenCols && correctAI) // take longer way
                    {
                        if (this.currentDirection != Direction.Down)
                        {
                            this.waitingDirection = Direction.Up;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Right)
                        {
                            this.waitingDirection = Direction.Left;
                        }
                    }
                }
            }
        }

        public void RunAwayFromPacman(Character pacman)
        {
            this.slowMotionMove = true;
            if (this.opponentStuck)
            {
                RunAwayWhenStuck();
            }
            else
            {
                RunAwayByNormalCalculatingDistanceDifference(pacman);
            }
        }

        private void RunAwayWhenStuck()
        {
            if (this.currentDirection == Direction.Up || this.currentDirection == Direction.Down)
            {
                if (new Random().Next(0, 2) == 1)
                {
                    this.waitingDirection = Direction.Left;
                }
                else
                {
                    this.waitingDirection = Direction.Right;
                }
            }
            if (this.currentDirection == Direction.Left || this.currentDirection == Direction.Right)
            {
                if (new Random().Next(0, 2) == 1)
                {
                    this.waitingDirection = Direction.Up;
                }
                else
                {
                    this.waitingDirection = Direction.Down;
                }
            }
        }

        private void RunAwayByNormalCalculatingDistanceDifference(Character pacman)
        {
            if (this.Position.Col > pacman.Position.Col)
            {
                int colDifference = this.Position.Col - pacman.Position.Col;

                if (this.Position.Row > pacman.Position.Row)
                {
                    int rowDifference = this.Position.Row - pacman.Position.Row;

                    if (colDifference > rowDifference)
                    {
                        if (this.currentDirection != Direction.Up)
                        {
                            this.waitingDirection = Direction.Down;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Left)
                        {
                            this.waitingDirection = Direction.Right;
                        }
                    }
                }
                else
                {
                    int rowDifference = pacman.Position.Row - this.Position.Row;

                    if (colDifference > rowDifference)
                    {
                        if (this.currentDirection != Direction.Down)
                        {
                            this.waitingDirection = Direction.Up;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Left)
                        {
                            this.waitingDirection = Direction.Right;
                        }
                    }
                }
            }
            else
            {
                int colDifference = pacman.Position.Col - this.Position.Col;

                if (this.Position.Row > pacman.Position.Row)
                {
                    int rowDifference = this.Position.Row - pacman.Position.Row;

                    if (colDifference > rowDifference)
                    {
                        if (this.currentDirection != Direction.Up)
                        {
                            this.waitingDirection = Direction.Down;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Right)
                        {
                            this.waitingDirection = Direction.Left;
                        }
                    }
                }
                else
                {
                    int rowDifference = pacman.Position.Row - this.Position.Row;

                    if (colDifference > rowDifference)
                    {
                        if (this.currentDirection != Direction.Down)
                        {
                            this.waitingDirection = Direction.Up;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Right)
                        {
                            this.waitingDirection = Direction.Left;
                        }
                    }
                }

            }
        }

        public override void Update()
        {
            if (this.StartFollow && this.startPosition == false)
            {
                SeeForStuck();
                this.oldPosition = new MatrixCoords(this.Position.Row, this.Position.Col);
                base.Update();
            }
        }

        private void SeeForStuck()
        {
            if (this.oldPosition == this.Position)
            {
                this.opponentStuck = true;
                this.counterForHardStuck++;
                if (this.counterForHardStuck == 4)
                {
                    this.opponentHardStuck = true;
                    this.counterForHardStuck = 1;
                }
                else
                {
                    this.opponentHardStuck = false;
                }
            }
            else
            {
                this.counterForHardStuck = 1;
                this.opponentStuck = false;
            }
        }

        public override void Move(Direction direction, int step = 1)
        {
            if (this.slowMotionMove)
            {
                if (counterForSlowMotionMove == 3)
                {
                    base.Move(direction, step);
                    counterForSlowMotionMove = 1;
                }
                else
                {
                    counterForSlowMotionMove++;
                }
            }
            else
            {
                base.Move(direction, step);
            }
        }

        public object Clone()
        {
            return new Opponent(this.Symbol, this.Position, this.levelOfSmart);
        }

        public override void Reset()
        {
            this.Position = new MatrixCoords(Constant.OpponentRowStartPosition, Constant.OpponentColStartPosition);
            this.startPosition = true;
            this.StartFollow = false;
            this.counterForHardStuck = 1;
            this.counterForSlowMotionMove = 1;
            base.Reset();
        }
    }
}
