﻿namespace Pacman.GameObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Enumerations;

    class Wall : GameObject
    {
        public Wall(char symbol, MatrixCoords position)
            :base(symbol, position)
        {
            base.team = Team.Wall;
        }
    }
}
