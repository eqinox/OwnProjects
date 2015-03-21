namespace Pacman.GameObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Enumerations;

    abstract class GameObject
    {
        protected Team team;
        protected char symbol;
        public MatrixCoords Position { get; set; }
        public bool IsAlive { get; set; }

        protected GameObject(char symbol, MatrixCoords position)
        {
            this.Symbol = symbol;
            this.Position = position;
            this.IsAlive = true;
        }

        public virtual char Symbol
        {
            get { return this.symbol; }
            set { this.symbol = value; }
        }

        /// <summary>
        /// Helps to recognize opponents, walls, scores and etc..
        /// </summary>
        public Team Team
        {
            get { return team; }
        }

        public virtual bool CanCollideWith(GameObject otherObj)
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

        public virtual void Update()
        {
        }
    }
}
