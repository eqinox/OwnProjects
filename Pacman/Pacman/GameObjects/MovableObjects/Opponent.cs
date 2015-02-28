namespace Pacman.GameObjects.MovableObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;
    using Pacman.Enumerations;

    class Opponent : MovableObject
    {
        private MatrixCoords oldPosition;
        private int counterForHardStuck = 1;
        private bool opponentHardStuck;
        private bool opponentStuck;
        private bool startPosition = true;

        public Opponent(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.team = Constant.OpponentTeam;
            this.oldPosition = new MatrixCoords(0, 0);
        }

        public void FollowCharacter(Character pacman)
        {
            if (startPosition)
            {
                this.waitingDirection = Direction.Up;
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
            if (this.currentDirection == Direction.Up || this.currentDirection == Direction.Down)
            {
                if (this.Position.Col > pacman.Position.Col) // if opponent is right and pacman left
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
                if (this.Position.Row > pacman.Position.Row) // if opponent is down and pacman up
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
            if (this.Position.Row <= pacman.Position.Row) // if opponent is up and pacman down
            {
                int differenceBetweenRows = pacman.Position.Row - this.Position.Row;

                if (this.Position.Col <= pacman.Position.Col) // if opponent is left and pacman right
                {
                    int differenceBetweenCols = pacman.Position.Col - this.Position.Col;

                    if (differenceBetweenRows >= differenceBetweenCols) // take longer way
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

                    if (differenceBetweenRows >= differenceBetweenCols) // take longer way
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

                    if (differenceBetweenRows >= differenceBetweenCols) // take longer way
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

                    if (differenceBetweenRows >= differenceBetweenCols) // take longer way
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
            SeeForStuck();
            this.oldPosition = new MatrixCoords(this.Position.Row, this.Position.Col);
            base.Update();
        }

        private void SeeForStuck()
        {
            if (this.oldPosition == this.Position)
            {
                this.startPosition = false;
                this.opponentStuck = true;
                this.counterForHardStuck++;
                if (this.counterForHardStuck == 4)
                {
                    this.opponentHardStuck = true;
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
    }
}
