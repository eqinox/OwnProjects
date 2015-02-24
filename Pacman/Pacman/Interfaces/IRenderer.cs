namespace Pacman.Interfaces
{
    using Pacman.GameObjects;
    using System.Collections.Generic;

    interface IRenderer
    {
        void EnqueueForRendering(GameObject obj);

        void EnqueueForRendering(ICollection<GameObject> objects);

        void RenderAll();

        void ClearQueue();
    }
}
