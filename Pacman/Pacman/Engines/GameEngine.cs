namespace Pacman.Engines
{
    using Pacman.GameObjects;
    using Pacman.GameObjects.MovableObjects;
    using Pacman.GameObjects.Scores;
    using Pacman.Interfaces;
    using System.Collections.Generic;
    using System.Threading;

    class GameEngine
    {
        private IUserInterface userInterface;
        private IRenderer renderer;
        private List<GameObject> allObjects;
        private List<Opponent> allOpponents;
        private Character pacman;
        private Map map;

        public GameEngine(IUserInterface userInterface, IRenderer renderer, Map map)
        {
            this.userInterface = userInterface;
            this.renderer = renderer;
            this.map = map;
            this.allObjects = new List<GameObject>();
            this.allOpponents = new List<Opponent>();
        }

        public void AddObject(GameObject obj)
        {
            if (obj is Opponent)
            {
                this.allOpponents.Add(obj as Opponent);
            }
            else if (obj is Character)
            {
                this.pacman = obj as Character;
            }

            this.allObjects.Add(obj);
        }

        public void Run()
        {
            while (true)
            {
                this.renderer.EnqueueForRendering(this.map.GiveMeMap());
                this.renderer.EnqueueForRendering(this.allObjects);
                
                this.renderer.RenderAll();

                this.userInterface.ProcessInput();

                SeeForAllCollisions(this.pacman, this.allOpponents, this.map.GiveMeAllWalls(), this.map.GiveMeAllScore());

                this.allObjects.RemoveAll(x => x.IsAlive == false);
                this.allOpponents.RemoveAll(x => x.IsAlive == false);

                this.renderer.ClearQueue();
                Thread.Sleep(150);
            }
        }

        private void SeeForAllCollisions(Character pacman, List<Opponent> allOpponents, List<Wall> allWalls, List<Score> allScores)
        {
            if (CollisionDispatcher.SeeForCollisionsWithOtherOpponent(pacman, allOpponents))
            {
                pacman.IsAlive = false;
            }

            List<GameObject> charactersToMoveBack = CollisionDispatcher.SeeForCollisionWithWalls(this.allObjects, allWalls);
            if (charactersToMoveBack != null)
            {
                foreach (var character in charactersToMoveBack)
                {
                    (character as MovableObject).MoveBack();
                }
            }

            Score score = CollisionDispatcher.SeeForCollisionWithScores(pacman, allScores);
            if (score != null)
            {

            }
        }
    }
}
