namespace Game.GameLogic.Scripts.RecorderUI
{
    using Game.Scripts.Replay;
    using UnityEngine;
    using UnityEngine.UI;

    public class StartRecordButton : Button
    {
        private readonly InjectField<ReplayService> _replayService = new();

        protected override void Start()
        {
            base.Start();
            onClick.AddListener(() =>
            {
                _replayService.Value.Init(FindObjectsByType<Transform>(FindObjectsSortMode.None));
                _replayService.Value.StartRecord();
            });
        }
    }
}