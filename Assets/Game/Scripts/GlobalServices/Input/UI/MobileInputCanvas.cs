namespace Game.Scripts.GlobalServices.Input.UI
{
    using Game.Scripts.GlobalServices.GameManager.MonoBeh;
    using UnityEngine;

    public class MobileInputCanvas : MonoBeh
    {
        [SerializeField] private ExtendedButton up;
        [SerializeField] private ExtendedButton down;
        [SerializeField] private ExtendedButton left;
        [SerializeField] private ExtendedButton right;
        [SerializeField] private ExtendedButton jump;

        public ExtendedButton Up => up;
        public ExtendedButton Down => down;
        public ExtendedButton Left => left;
        public ExtendedButton Right => right;
        public ExtendedButton Jump => jump;
    }
}