namespace Game.GameLogic.Scripts.Services
{
    using UnityEngine;

    public class InputService
    {
        public NetworkInputData GetData()
        {
            var data = new NetworkInputData
            {
                MovementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")),
                RotationDirection = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))
            };
            return data;
        }
    }
}