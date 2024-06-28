namespace Game.GameLogic.Scripts.RecorderUI
{
    using System.Collections;
    using Game.Scripts.Replay;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class PlayRecordButton : MonoBehaviour
    {
        private static readonly int _mainTex = Shader.PropertyToID("_MainTex");
        [SerializeField] private Material material;

        private readonly InjectField<ReplayService> _replayService = new();

        protected void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
                StartCoroutine(Play())
            );
        }

        public IEnumerator Play(float speed = 1f)
        {
            var pngs = _replayService.Value.Pngs;
            var startTime = Time.time;

            var waitForFixedUpdate = new WaitForFixedUpdate();
            var t = new Texture2D(ReplayService.Size.width, ReplayService.Size.height, TextureFormat.RGBA32, false);
            while (FrameIndex() < pngs.Count)
            {
                if (!t.LoadImage(pngs[FrameIndex()]))
                {
                    print("Invalid frame");
                    yield return null;
                }
                material.SetTexture(_mainTex, t);

                yield return waitForFixedUpdate;
            }

            yield break;

            int FrameIndex() => (int)((Time.time - startTime) * speed / ReplayService.TimeFrame);
        }
    }
}