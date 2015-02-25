namespace Pacman.GameObjects
{
    using Pacman.ConsoleThings;

    abstract class GameObject
    {
        protected int team;
        private MatrixCoords position;
        private char symbol;
        private bool isAlive;

        public GameObject(char symbol, MatrixCoords position)
        {
            this.Symbol = symbol;
            this.Position = position;
            this.IsAlive = true;
        }

        public MatrixCoords Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Helps to recognize opponents, walls, scores and etc..
        /// </summary>
        public int Team
        {
            get { return team; }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public bool CanCollideWith(GameObject otherObj)
        {
            if (this.Position == otherObj.Position)
            {
                if (this.Team != otherObj.Team)
                {
                    return true;
                }
            }

            return false;
        }

        public void Update()
        {

        }
    }
}
