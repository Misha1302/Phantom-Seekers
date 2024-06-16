namespace Game.GameLogic.Scripts
{
    using UnityEngine;

    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private float horizontalSpeed = 5;
        [SerializeField] private float verticalSpeed = 5;

        private readonly InjectField<InputService> _inputService = new();

        private Transform _player;

        private void Start()
        {
            _player = FindAnyObjectByType<Player>().transform;
        }

        private void LateUpdate()
        {
            var vec = _inputService.Value.GetData().RotationDirection * Time.deltaTime;
            _player.Rotate(new Vector3(0, vec.x * horizontalSpeed));
            transform.Rotate(new Vector3(-vec.y * verticalSpeed, 0));
        }
    }
}