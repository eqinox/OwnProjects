namespace Pacman.Interfaces
{
    using Pacman.GameObjects;
    using Pacman.GameObjects.MovableObjects;
    using System.Collections.Generic;

    interface IRenderer
    {
        void EnqueueForRendering(GameObject obj);

        void EnqueueForRendering(ICollection<GameObject> objects);

        void EnqueueForRendering(ICollection<Opponent> objects);

        void RenderAll();

        void ClearQueue();
    }
}
