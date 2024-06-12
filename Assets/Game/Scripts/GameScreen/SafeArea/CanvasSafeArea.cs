namespace Game.Scripts.GameScreen.SafeArea
{
    using Game.Scripts.Extensions;
    using Game.Scripts.GlobalServices.GameManager.MonoBeh;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    ///     <a href="https://forum.unity.com/threads/canvashelper-resizes-a-recttransform-to-iphone-xs-safe-area.521107/">
    ///         code taken from here
    ///     </a>
    /// </summary>
    public class CanvasSafeArea : MonoBeh
    {
        private static bool _screenChangeVarsInitialized;
        private static ScreenOrientation _lastOrientation;
        private static Vector2Int _lastResolution;
        private static Rect _lastSafeArea;

        private Canvas _canvas;

        private void OnValidate()
        {
#if UNITY_EDITOR
            EditorApplication.delayCall += SetSafeAreaCharacteristics;
#endif
        }

        private void SetSafeAreaCharacteristics()
        {
            var rectTransform = transform.GetOrAddComponent<RectTransform>();
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        protected override void Tick()
        {
            var a = Screen.orientation == _lastOrientation;
            var b = Screen.safeArea == _lastSafeArea;
            var c = Screen.width == _lastResolution.x && Screen.height == _lastResolution.y;

            if (a && b && c) return;

            OnOrientationChanged();
            ApplySafeArea();
            OnResolutionChanged();
        }

        protected override void OnStart()
        {
            SetSafeAreaCharacteristics();

            _canvas = GetComponentInParent<Canvas>();

            SetStaticsIfNeed();

            ApplySafeArea();
        }

        private static void SetStaticsIfNeed()
        {
            if (_screenChangeVarsInitialized) return;

            _lastOrientation = Screen.orientation;
            _lastResolution.x = Screen.width;
            _lastResolution.y = Screen.height;
            _lastSafeArea = Screen.safeArea;

            _screenChangeVarsInitialized = true;
        }

        private void ApplySafeArea()
        {
            if (this == null || gameObject == null)
                return;

            GetAnchors(out var anchorMin, out var anchorMax);

            var safeAreaTransform = GetComponent<RectTransform>();
            safeAreaTransform.anchorMin = anchorMin;
            safeAreaTransform.anchorMax = anchorMax;
        }

        private void GetAnchors(out Vector2 anchorMin, out Vector2 anchorMax)
        {
            var safeArea = Screen.safeArea;
            anchorMin = GetAnchor(safeArea.position);
            anchorMax = GetAnchor(safeArea.position + safeArea.size);
        }

        private Vector2 GetAnchor(Vector2 startAnchor)
        {
            startAnchor.x /= _canvas.pixelRect.width;
            startAnchor.y /= _canvas.pixelRect.height;
            return startAnchor;
        }

        private static void OnOrientationChanged()
        {
            _lastOrientation = Screen.orientation;
        }

        private static void OnResolutionChanged()
        {
            _lastResolution.x = Screen.width;
            _lastResolution.y = Screen.height;
        }
    }
}