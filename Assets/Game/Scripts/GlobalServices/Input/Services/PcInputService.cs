namespace Game.Scripts.GlobalServices.Input.Services
{
    using UnityEngine;

    public class PcInputService : InputServiceBase
    {
        protected override void TickAnyway()
        {
            Input.X.HandleDirection(UnityEngine.Input.GetAxisRaw("Horizontal"));
            Input.Y.HandleDirection(UnityEngine.Input.GetAxisRaw("Vertical"));

            Jump = UnityEngine.Input.GetKey(KeyCode.Space);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
                OnPause?.Invoke();
        }
    }
}