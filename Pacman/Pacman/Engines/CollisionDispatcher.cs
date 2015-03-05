namespace Pacman.Engines
{
    using Pacman.GameObjects;
    using Pacman.GameObjects.MovableObjects;
    using Pacman.GameObjects.Scores;
    using System.Collections.Generic;

    class CollisionDispatcher
    {
        public static MovableObject SeeForCollisionWithOpponent(Character pacman, List<MovableObject> allOppponents)
        {
            foreach (var opponent in allOppponents)
            {
                if (pacman.CanCollideWith(opponent))
                {
                    return opponent;
                }
            }

            return null;
        }

        /// <summary>
        /// The method checks if given GameObject crashed into a wall
        /// </summary>
        /// <param name="movableObject"></param>
        /// <param name="allWalls"></param>
        /// <returns>The method returns all GameObjects which crashed into a wall and should call method MoveBack();</returns>
        public static bool SeeForCollisionWithWalls(MovableObject movableObject, List<Wall> allWalls)
        {
            foreach (var wall in allWalls)
            {
                if (movableObject.CanCollideWith(wall))
                {
                    return true;
                }
            }

            return false;
        }

        public static GameObject SeeForCollisionWithObjects(Character pacman, List<GameObject> allObjects)
        {
            foreach (var obj in allObjects)
            {
                if (pacman.CanCollideWith(obj))
                {
                    return obj;
                }
            }

            return null;
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
