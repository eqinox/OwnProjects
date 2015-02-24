namespace Pacman.Engines
{
    using Pacman.GameObjects;
    using Pacman.Interfaces;
using System.Collections.Generic;
    using System.Threading;

    class GameEngine
    {
        private IUserInterface userInterface;
        private IRenderer renderer;
        private List<GameObject> allObjects;

        public GameEngine(IUserInterface userInterface, IRenderer renderer)
        {
            this.userInterface = userInterface;
            this.renderer = renderer;
            this.allObjects = new List<GameObject>();
        }

        public void AddObject(GameObject obj)
        {
            this.allObjects.Add(obj);
        }

        public void Run()
        {
            while (true)
            {
                foreach (var obj in this.allObjects)
                {
                    this.renderer.EnqueueForRendering(this.allObjects);
                }

                this.renderer.RenderAll();

                this.userInterface.ProcessInput();

                this.renderer.ClearQueue();
                Thread.Sleep(150);
            }
        }
    }
}
