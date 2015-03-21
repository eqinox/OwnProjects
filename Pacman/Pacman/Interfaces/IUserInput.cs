namespace Pacman.Interfaces
{
    using System;

    interface IUserInput
    {
        event EventHandler OnLeftPressed;

        event EventHandler OnRightPressed;

        event EventHandler OnUpPressed;

        event EventHandler OnDownPressed;

        void ProcessInput();
    }
}
