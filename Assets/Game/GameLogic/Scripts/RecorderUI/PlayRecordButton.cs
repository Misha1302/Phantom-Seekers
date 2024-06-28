namespace Game.GameLogic.Scripts.RecorderUI
{
    using System.Collections;
    using Game.Scripts.Helpers;
    using Game.Scripts.Replay;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class PlayRecordButton : MonoBehaviour
    {
        [SerializeField] private RenderTexture renderTexture;
        [SerializeField] private GameObject videoPlayer;

        private readonly InjectField<ReplayService> _replayService = new();

        protected void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
                StartCoroutine(Play(renderTexture))
            );
        }

        public IEnumerator Play(RenderTexture textureToRender, float speed = 1f)
        {
            var list = _replayService.Value.Compile();
            var startTime = Time.time;

            var oldMainCamera = FindAnyObjectByType<Camera>() ??
                                Thrower.InvalidOpEx("Cannot find main camera").Get<Camera>();
            oldMainCamera.targetTexture = textureToRender;
            oldMainCamera.targetDisplay = 100;

            var videoPlayerClone = Instantiate(videoPlayer);

            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (FrameIndex() < list.Frames.Count)
            {
                foreach (var obj in list.Frames[FrameIndex()].Objs)
                {
                    if (obj.Item1 == null)
                    {
                        Debug.LogWarning($"{obj} was destroyed");
                        continue;
                    }

                    if (obj.Item1.transform.name.Contains("Camera"))
                        print(obj.Item2.Pos);

                    obj.Item1.position = obj.Item2.Pos;
                    obj.Item1.eulerAngles = obj.Item2.Rot;
                    obj.Item1.localScale = obj.Item2.Scale;
                }

                yield return waitForFixedUpdate;
            }

            Destroy(videoPlayerClone.gameObject);
            oldMainCamera.targetDisplay = 0;
            oldMainCamera.targetTexture = null;
            yield break;

            int FrameIndex() =>
                (int)((Time.time - startTime) * speed / (1f / 30f));
        }
    }
}