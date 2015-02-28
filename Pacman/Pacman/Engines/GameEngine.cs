namespace Pacman.Engines
{
    using Pacman.GameObjects;
    using Pacman.GameObjects.MovableObjects;
    using Pacman.GameObjects.Scores;
    using Pacman.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    class GameEngine
    {
        // If we create list, we should delete all dead (IsAlive == false) objects from it in RemoveAllDeadObjects() method
        private IUserInterface userInterface;
        private IRenderer renderer;
        private List<GameObject> allObjects;
        private List<Opponent> allOpponents;
        private List<MovableObject> allMovableObjects;
        private Character pacman;
        private Map map;

        public GameEngine(IUserInterface userInterface, IRenderer renderer, Map map)
        {
            this.userInterface = userInterface;
            this.renderer = renderer;
            this.map = map;
            this.allObjects = new List<GameObject>();
            this.allOpponents = new List<Opponent>();
            this.allMovableObjects = new List<MovableObject>();
        }

        public void AddObject(GameObject obj)
        {
            if (obj is Opponent)
            {
                this.allOpponents.Add(obj as Opponent);
            }
            if (obj is Character)
            {
                this.pacman = obj as Character;
            }
            if (obj is MovableObject)
            {
                this.allMovableObjects.Add(obj as MovableObject);
            }

            this.allObjects.Add(obj);
        }

        public void Run()
        {
            while (true)
            {
                this.renderer.EnqueueForRendering(this.map.GiveMeMap());
                this.renderer.EnqueueForRendering(this.allObjects);

                this.userInterface.ProcessInput();

                foreach (var opponent in this.allOpponents)
                {
                    opponent.FollowCharacter(this.pacman);
                }

                foreach (var obj in this.allMovableObjects)
                {
                    obj.SetIfCanMoveToWaitingDirection(this.map.GiveMeMap());
                    obj.Update();
                }

                this.renderer.RenderAll();
                RenderScore();

                SeeForAllCollisions(this.pacman, this.allOpponents, this.map.GiveMeAllWalls(), this.map.GiveMeAllScores());

                RemoveAllDeadObjects();

                this.renderer.ClearQueue();
                Thread.Sleep(150);
            }
        }

        private void RemoveAllDeadObjects()
        {
            this.allObjects.RemoveAll(x => x.IsAlive == false);
            this.allOpponents.RemoveAll(x => x.IsAlive == false);
            this.allMovableObjects.RemoveAll(x => x.IsAlive == false);
            this.map.GiveMeMap().RemoveAll(x => x.IsAlive == false);
            this.map.GiveMeAllScores().RemoveAll(x => x.IsAlive == false);
        }

        private void SeeForAllCollisions(Character pacman, List<Opponent> allOpponents, List<Wall> allWalls, List<Score> allScores)
        {
            if (CollisionDispatcher.SeeForCollisionsWithOtherOpponent(pacman, allOpponents))
            {
                pacman.IsAlive = false;
            }

            List<MovableObject> charactersToMoveBack = CollisionDispatcher.SeeForCollisionWithWalls(this.allMovableObjects, allWalls);
            if (charactersToMoveBack != null)
            {
                foreach (var character in charactersToMoveBack)
                {
                    character.MoveBack();
                }
            }

            Score score = CollisionDispatcher.SeeForCollisionWithScores(pacman, allScores);
            if (score != null)
            {
                pacman.TakeScore(score);
            }
        }

        private void RenderScore()
        {
            string scoreMsg = string.Format("Scores: {0}", this.pacman.Scores);
            Console.SetCursorPosition(this.map.AllColls / 2 - scoreMsg.Length / 2, Console.CursorTop);
            Console.WriteLine(scoreMsg);
        }
    }
}
