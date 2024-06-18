namespace Game.GameLogic.Scripts
{
    using UnityEngine;

    public class CameraFollower : MonoBehaviour
    {
        private Vector3 _offset;
        private Transform _target;

        private void LateUpdate()
        {
            if (_target == null) return;

            transform.position = _target.position + _offset;
        }

        public CameraFollower SetTarget(Transform target)
        {
            _target = target;
            return this;
        }

        public CameraFollower SetOffset(Vector3 offset)
        {
            _offset = offset;
            return this;
        }
    }
}