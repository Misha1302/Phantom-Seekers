namespace Game.GameLogic.Scripts
{
    using UnityEngine;

    public class CursorService
    {
        public void Hide()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Show()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}