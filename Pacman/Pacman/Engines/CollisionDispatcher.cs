namespace Pacman.Engines
{
    using Pacman.GameObjects;
    using Pacman.GameObjects.MovableObjects;
    using Pacman.GameObjects.Scores;
    using System.Collections.Generic;

    class CollisionDispatcher
    {
        public static bool SeeForCollisionsWithOtherOpponent(Character pacman, List<Opponent> allOpponents)
        {
            foreach (var opponent in allOpponents)
            {
                if (pacman.CanCollideWith(opponent))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The method checks if given GameObject crashed into a wall
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="allWalls"></param>
        /// <returns>The method returns all GameObjects which crashed into a wall and should call method MoveBack();</returns>
        public static List<MovableObject> SeeForCollisionWithWalls(List<MovableObject> characters, List<Wall> allWalls)
        {
            List<MovableObject> allCharactersToMoveBack = new List<MovableObject>();

            foreach (var character in characters)
            {
                foreach (var wall in allWalls)
                {
                    if (character.CanCollideWith(wall))
                    {
                        allCharactersToMoveBack.Add(character);
                        break;
                    }
                }
            }

            return allCharactersToMoveBack;
        }

        public static Score SeeForCollisionWithScores(Character pacman, List<Score> allScores)
        {
            foreach (var score in allScores)
            {
                if (pacman.CanCollideWith(score))
                {
                    return score;
                }
            }

            return null;
        }
    }
}
