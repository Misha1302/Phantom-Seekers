namespace Game.GameLogic.Scripts
{
    using Fusion;
    using Game.Scripts.Extensions.Math.Vectors;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private Vector3 speed = new(1f, 0, 5f);
        private readonly InjectField<InputService> _inputService = new();
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public override void FixedUpdateNetwork()
        {
            if (!HasStateAuthority)
                return;

            var direction = _inputService.Value.GetData().MovementDirection;
            direction.Scale(speed * Runner.DeltaTime);
            direction = _rb.transform.TransformDirection(direction);
            _rb.velocity = direction.WithY(_rb.velocity.y);
        }
    }
}