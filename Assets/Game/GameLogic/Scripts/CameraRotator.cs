namespace Game.GameLogic.Scripts
{
    using Game.GameLogic.Scripts.Services;
    using Game.Scripts.Extensions.Math.Vectors;
    using UnityEngine;

    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private float horizontalSpeed = 5;
        [SerializeField] private float verticalSpeed = 5;

        private readonly InjectField<InputService> _inputService = new();

        private Transform _player;

        private void LateUpdate()
        {
            if (_player == null) return;

            var vec = _inputService.Value.GetData().RotationDirection * Time.deltaTime;

            _player.Rotate(new Vector3(0, vec.x * horizontalSpeed));
            transform.eulerAngles = transform.eulerAngles.WithY(_player.eulerAngles.y);
            transform.Rotate(new Vector3(-vec.y * verticalSpeed, 0));
        }

        public CameraRotator SetTarget(Transform player)
        {
            _player = player;
            return this;
        }
    }
}