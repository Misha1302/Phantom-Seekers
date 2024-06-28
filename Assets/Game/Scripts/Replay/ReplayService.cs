namespace Game.Scripts.Replay
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ReplayService : MonoBehaviour
    {
        private readonly List<Frame> _frames = new(30 * 60 * 2);
        private Transform[] _objectsToTrack;
        private bool _recording;

        private float _timeSinceUpdate;

        public void FixedUpdate()
        {
            if (!_recording) return;

            _timeSinceUpdate += Time.deltaTime;
            if (_timeSinceUpdate >= 1f / 30f)
            {
                _timeSinceUpdate = 0f;
                SaveFrame();
            }
        }

        public void Init(Transform[] objectsToTrack)
        {
            _objectsToTrack = objectsToTrack;
            DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public void StartRecord()
        {
            _recording = true;
        }

        public void EndRecording()
        {
            _recording = false;
        }

        private void SaveFrame()
        {
            var frame = new Frame(_objectsToTrack.Length);
            foreach (var obj in _objectsToTrack)
                frame.Add(obj, new SavableData(obj.position, obj.eulerAngles, obj.localScale));

            _frames.Add(frame);
        }

        public ReplayUnit Compile() => new(_frames);
    }
}