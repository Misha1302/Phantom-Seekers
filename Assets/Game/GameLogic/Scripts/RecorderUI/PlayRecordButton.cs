namespace Game.GameLogic.Scripts.RecorderUI
{
    using System;
    using System.Collections;
    using Game.Scripts.Replay;
    using UnityEngine.UI;

    public class PlayRecordButton : Button
    {
        private readonly InjectField<ReplayService> _replayService = new();

        protected override void Start()
        {
            base.Start();
            onClick.AddListener(() =>
                StartCoroutine(Play())
            );
        }

        public IEnumerator Play(float speed = 1f)
        {
            var list = _replayService.Value.Compile();
            var startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            while (FrameIndex() < list.Frames.Count)
            {
                foreach (var obj in list.Frames[FrameIndex()].Objs)
                {
                    obj.Item1.position = obj.Item2.Pos;
                    obj.Item1.eulerAngles = obj.Item2.Rot;
                    obj.Item1.localScale = obj.Item2.Scale;
                }

                yield return null;
            }

            yield break;

            int FrameIndex() =>
                (int)((DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime) * speed / (1f / 30f * 1000));
        }
    }
}