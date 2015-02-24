namespace Pacman.GameObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Enumerations;
    using Pacman.Interfaces;
    using System;

    class MovableObject : GameObject, IMovable
    {
        private MatrixCoords oldPosition;

        public MovableObject(char name, MatrixCoords position)
            :base(name, position)
        {

        }

        public virtual void Move(Direction direction, int step = 1)
        {
            int newRow = this.Position.Row;
            int newCol = this.Position.Col;
            this.oldPosition = new MatrixCoords(this.Position.Row, this.Position.Col);

            switch (direction)
            {
                case Direction.Up:
                    if (newRow - step >= 0)
                    {
                        newRow -= step;
                    }
                    break;

                case Direction.Right:
                    if (newCol + step < ConsoleSettings.ConsoleWidth)
                    {
                        newCol += step;
                    }
                    break;

                case Direction.Down:
                    if (newRow + step < ConsoleSettings.ConsoleHeight)
                    {
                        newRow += step;
                    }
                    break;

                case Direction.Left:
                    if (newCol - step >= 0)
                    {
                        newCol -= step;
                    }
                    break;

                default:
                    throw new InvalidOperationException("Invalid direction provided.");
            }
            this.Position = new MatrixCoords(newRow, newCol);
        }

        public void MoveBack()
        {
            this.Position = new MatrixCoords(this.oldPosition.Col, this.oldPosition.Row);
        }
    }
}
