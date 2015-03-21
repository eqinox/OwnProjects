namespace Pacman.ConsoleThings
{
    class MatrixCoords
    {
        private int row;
        private int col;

        public MatrixCoords(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public MatrixCoords TopPosition
        {
            get
            {
                return new MatrixCoords(this.Row - 1, this.Col);
            }
        }

        public MatrixCoords BottomPosition
        {
            get
            {
                return new MatrixCoords(this.Row + 1, this.col);
            }
        }

        public MatrixCoords LeftPosition
        {
            get
            {
                return new MatrixCoords(this.Row, this.Col - 1);
            }
        }

        public MatrixCoords RightPosition
        {
            get
            {
                return  new MatrixCoords(this.Row, this.Col + 1);
            }
        }

        public int Col
        {
            get { return col; }
            set { col = value; }
        }

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        public static MatrixCoords operator +(MatrixCoords a, MatrixCoords b)
        {
            return new MatrixCoords(a.Row + b.Row, a.Col + b.Col);
        }

        public static MatrixCoords operator -(MatrixCoords a, MatrixCoords b)
        {
            return new MatrixCoords(a.Row - b.Row, a.Col - b.Col);
        }

        public static bool operator ==(MatrixCoords a, MatrixCoords b)
        {
            if (object.ReferenceEquals(a, null))
            {
                return false;
            }
            if (object.ReferenceEquals(b, null))
            {
                return false;
            }
            if (a.Row == b.Row && a.Col == b.Col)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(MatrixCoords a, MatrixCoords b)
        {
            if (object.ReferenceEquals(a, null))
            {
                return false;
            }
            if (object.ReferenceEquals(b, null))
            {
                return false;
            }
            if (a.Row == b.Row && a.Col == b.Col)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public override string ToString()
        {
            return string.Format("Row[{0}], Col[{1}]", this.Row, this.Col);
        }
    }
}
