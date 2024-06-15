namespace Game.GameLogic.Scripts
{
    using Fusion;
    using UnityEngine;

    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private float speed = 5;
        private NetworkCharacterController _cc;

        private void Awake()
        {
            _cc = GetComponent<NetworkCharacterController>();
        }

        public override void FixedUpdateNetwork()
        {
            if (!GetInput(out NetworkInputData data)) return;

            data.Direction.Normalize();
            _cc.Move(data.Direction * Runner.DeltaTime * speed);

            if (data.Jump) _cc.Jump();
        }
    }
}