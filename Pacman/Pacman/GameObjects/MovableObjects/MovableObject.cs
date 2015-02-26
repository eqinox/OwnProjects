namespace Pacman.GameObjects.MovableObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;
    using Pacman.Enumerations;
    using Pacman.GameObjects.Scores;
    using Pacman.Interfaces;
    using System;
    using System.Collections.Generic;

    abstract class MovableObject : GameObject, IMovable
    {
        private MatrixCoords oldPosition;
        public Direction currentDirection;
        public Direction waitingDirection; // for easy character moving
        
        public MovableObject(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            this.currentDirection = Direction.Right;
            this.waitingDirection = Direction.Right;
        }

        public virtual void Move(Direction direction, int step = 1)
        {
            int newRow = this.Position.Row;
            int newCol = this.Position.Col;
            this.oldPosition = new MatrixCoords(this.Position.Row, this.Position.Col);

            switch (direction)
            {
                case Direction.Up: newRow -= step; break;
                case Direction.Down: newRow += step; break;
                case Direction.Right: newCol += step; break;
                case Direction.Left: newCol -= step; break;
                default:
                    throw new InvalidOperationException("Invalid direction provided.");
            }
            this.Position = new MatrixCoords(newRow, newCol);
        }

        public void MoveBack()
        {
            this.Position = new MatrixCoords(this.oldPosition.Row, this.oldPosition.Col);
        }

        public override void Update()
        {
            Move(this.currentDirection);
        }

        public void SetIfCanMoveToWaitingDirection(List<GameObject> otherObj)
        {
            if (this.waitingDirection == Direction.Right)
            {
                GameObject currObj = otherObj.Find(x =>
                    (this.Position.Col + 2 == x.Position.Col) &&
                    (this.Position.Row == x.Position.Row)
                    );
                if (currObj != null)
                {
                    if (!(currObj is Wall))
                    {
                        this.currentDirection = this.waitingDirection;
                    }
                }
                else
                {
                    this.currentDirection = this.waitingDirection;
                }
            }
            else if (this.waitingDirection == Direction.Left)
            {
                GameObject currObj = otherObj.Find(x =>
                    (this.Position.Col - 2 == x.Position.Col) &&
                    (this.Position.Row == x.Position.Row)
                    );
                if (currObj != null)
                {
                    if (!(currObj is Wall))
                    {
                        this.currentDirection = this.waitingDirection;
                    }
                }
                else
                {
                    this.currentDirection = this.waitingDirection;
                }
            }
            else if (this.waitingDirection == Direction.Up)
            {
                GameObject currObj = otherObj.Find(x =>
                    (this.Position.Row - 1 == x.Position.Row) &&
                    (this.Position.Col == x.Position.Col)
                    );
                if (currObj != null)
                {
                    if (!(currObj is Wall))
                    {
                        this.currentDirection = this.waitingDirection;
                    }
                }
                else
                {
                    this.currentDirection = this.waitingDirection;
                }
            }
            else if (this.waitingDirection == Direction.Down)
            {
                GameObject currObj = otherObj.Find(x =>
                    (this.Position.Row + 1 == x.Position.Row) &&
                    (this.Position.Col == x.Position.Col)
                    );
                if (currObj != null)
                {
                    if (!(currObj is Wall))
                    {
                        this.currentDirection = this.waitingDirection;
                    }
                }
                else
                {
                    this.currentDirection = this.waitingDirection;
                }
            }
        }
    }
}
