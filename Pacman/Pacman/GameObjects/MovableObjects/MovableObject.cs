namespace Pacman.GameObjects.MovableObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Enumerations;
    using Pacman.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        public virtual void Move(Direction direction)
        {
            int newRow = this.Position.Row;
            int newCol = this.Position.Col;
            this.oldPosition = new MatrixCoords(this.Position.Row, this.Position.Col);

            switch (direction)
            {
                case Direction.Up: newRow -= 1; break;
                case Direction.Down: newRow += 1; break;
                case Direction.Right: newCol += 1; break;
                case Direction.Left: newCol -= 1; break;
                default:
                    throw new InvalidOperationException("Invalid direction provided.");
            }
            this.Position = new MatrixCoords(newRow, newCol);
        }
            
        public void MoveBack()
        {
            this.Position = new MatrixCoords(this.oldPosition.Row, this.oldPosition.Col);
        }

        public void SetWaitingDirection(Direction direction)
        {
            this.waitingDirection = direction;
        }

        public override void Update()
        {
            Move(this.currentDirection);
        }

        public void SetIfCanMoveToWaitingDirection(List<Path> otherObj)
        {
            if (this.waitingDirection == Direction.Up)
            {
                if (otherObj.FirstOrDefault(x => x.Position == this.Position.TopPosition) != null)
                {
                    this.currentDirection = Direction.Up;
                }
            }
            else if (this.waitingDirection == Direction.Down)
            {
                if (otherObj.FirstOrDefault(x => x.Position == this.Position.BottomPosition) != null)
                {
                    this.currentDirection = Direction.Down;
                }
            }
            else if (this.waitingDirection == Direction.Left)
            {
                if (otherObj.FirstOrDefault(x => x.Position == this.Position.LeftPosition) != null)
                {
                    this.currentDirection = Direction.Left;
                }
            }
            else if (this.waitingDirection == Direction.Right)
            {
                if (otherObj.FirstOrDefault(x => x.Position == this.Position.RightPosition) != null)
                {
                    this.currentDirection = Direction.Right;
                }
            }
        }

        public virtual void Reset()
        {
            this.IsAlive = true;
        }
    }
}
