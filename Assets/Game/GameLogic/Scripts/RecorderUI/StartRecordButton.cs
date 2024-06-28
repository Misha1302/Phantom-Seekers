namespace Game.GameLogic.Scripts.RecorderUI
{
    using Game.Scripts.Replay;
    using UnityEngine.UI;

    public class StartRecordButton : Button
    {
        private readonly InjectField<ReplayService> _replayService = new();

        protected override void Start()
        {
            base.Start();
            onClick.AddListener(() => _replayService.Value.StartRecord());
        }
    }
}