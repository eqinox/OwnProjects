namespace Pacman.GameObjects
{
    using Pacman.ConsoleThings;

    abstract class GameObject
    {
        private MatrixCoords position;
        private char symbol;

        public GameObject(char symbol, MatrixCoords position)
        {
            this.Symbol = symbol;
            this.Position = position;
        }

        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public MatrixCoords Position
        {
            get { return position; }
            set { position = value; }
        }

    }
}
