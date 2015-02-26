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
