namespace Game.GameLogic.Scripts.RecorderUI
{
    using System.Linq;
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
                var objectsToTrack = FindObjectsByType<Transform>(FindObjectsSortMode.None)
                    .Select(x => x.root).Distinct().ToArray();
                _replayService.Value.Init(objectsToTrack);
                _replayService.Value.StartRecord();
            });
        }
    }
}