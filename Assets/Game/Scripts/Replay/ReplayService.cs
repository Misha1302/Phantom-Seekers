namespace Game.Scripts.Replay
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Experimental.Rendering;

    public class ReplayService : MonoBehaviour
    {
        private readonly List<byte[]> _pngs = new();
        private bool _recording;
        private RenderTexture _renderTexture;
        private Texture2D _tex;

        private float _timeSinceUpdate;

        public const float TimeFrame = 1 / 30f;

        public static readonly (int width, int height) Size = (1920 / 3, 1080 / 3);
        private Camera _cam;

        public IReadOnlyList<byte[]> Pngs => _pngs;

        private void Start()
        {
            _renderTexture = new RenderTexture(Size.width, Size.height, GraphicsFormat.R8G8B8A8_UNorm,
                GraphicsFormat.D32_SFloat_S8_UInt);

            _tex = new Texture2D(_renderTexture.width, _renderTexture.height);
            _cam = FindAnyObjectByType<Camera>();
        }

        public void FixedUpdate()
        {
            if (!_recording) return;

            _timeSinceUpdate += Time.deltaTime;

            if (_timeSinceUpdate < TimeFrame) return;

            _timeSinceUpdate = 0f;
            SavePng();
        }


        private void SavePng()
        {
            RenderTexture.active = _renderTexture;
            _cam.targetTexture = _renderTexture;
            _cam.Render();
            _cam.targetTexture = null;
            _tex.ReadPixels(new Rect(0, 0, _renderTexture.width, _renderTexture.height), 0, 0);
            _tex.Apply();
            RenderTexture.active = null;
            // _tex.GetRawTextureData()!
            _pngs.Add(_tex.EncodeToPNG());
        }

        public void StartRecord()
        {
            _recording = true;
            _pngs.Clear();
        }

        public void EndRecording()
        {
            _recording = false;
        }
    }
}