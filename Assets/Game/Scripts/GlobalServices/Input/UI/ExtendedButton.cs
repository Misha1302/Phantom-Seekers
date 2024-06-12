namespace Game.Scripts.GlobalServices.Input.UI
{
    using UnityEngine.UI;

    public class ExtendedButton : Button
    {
        public bool WasPressed => IsPressed();
    }
}