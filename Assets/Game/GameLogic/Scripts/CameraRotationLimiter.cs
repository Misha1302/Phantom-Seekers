namespace Game.GameLogic.Scripts
{
    using Game.Scripts.Extensions;
    using Game.Scripts.Extensions.Math.Numbers;
    using Game.Scripts.Extensions.Math.Vectors;
    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    public class CameraRotationLimiter : MonoBehaviour
    {
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;

        private void LateUpdate()
        {
            var angles = transform.eulerAngles;
            transform.eulerAngles = angles.WithX(GetX(angles).Clamp(minAngle, maxAngle));
        }

        private float GetX(Vector3 angles) => angles.x.ThisOrIf(x => x > 180, angles.x - 360);
    }
}