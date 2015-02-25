namespace Pacman.GameObjects.MovableObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;
    using Pacman.Enumerations;
    using Pacman.Interfaces;
    using System;

    abstract class MovableObject : GameObject, IMovable
    {
        private MatrixCoords oldPosition;
        
        public MovableObject(char symbol, MatrixCoords position)
            :base(symbol, position)
        {

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
    }
}
