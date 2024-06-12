namespace Game.Scripts.GlobalServices.Input.Services
{
    using System;
    using Game.Scripts.Attributes;
    using Game.Scripts.GlobalServices.Creator;
    using Game.Scripts.GlobalServices.GameManager.MonoBeh;
    using Game.Scripts.GlobalServices.Input.Axis;
    using Game.Scripts.GlobalServices.Pause.Canvas;
    using Game.Scripts.GlobalServices.Repository;
    using UnityEngine;
    using UnityEngine.UI;

    public abstract class InputServiceBase : MonoBeh, IInputService
    {
        // ReSharper disable UnassignedField.Global
        [Inject] protected CreatorService CreatorService;
        [Inject] protected RepositoryService RepositoryService;

        public Axis2D Input { get; private set; }
        public bool Jump { get; protected set; }
        public Action OnPause { get; set; }


        protected override void OnCreated()
        {
            Input = new Axis2D(RepositoryService.GameData.inputSpeed.Value);
        }

        protected override void OnStart()
        {
            SubscribeOnButtonPause();
        }

        protected static float CalcDir(bool neg, bool pos)
        {
            float dir = 0;
            if (pos) dir++;
            if (neg) dir--;
            return dir;
        }

        private void SubscribeOnButtonPause()
        {
            var pauseCanvas = Resources.Load<GameObject>("UI/PauseCanvas");
            var pauseCanvasInstance = CreatorService.Instantiate(pauseCanvas);
            var button = pauseCanvasInstance.GetComponentInChildren<PauseButtonTag>().GetComponent<Button>();
            button.onClick.AddListener(OnPause.Invoke);
        }
    }
}