namespace Game.Scripts.GlobalServices.FpsCounter.Visualizer
{
    using Game.Scripts.Attributes;
    using Game.Scripts.Extensions;
    using Game.Scripts.GlobalServices.GameManager.MonoBeh;
    using TMPro;

    public class FpsVisualizer : MonoBeh
    {
        [Inject] private FpsCounterService _fpsCounterService;
        private TMP_Text _text;

        protected override void OnStart()
        {
            _text = GetComponentInChildren<FpsTextTag>().GetComponent<TMP_Text>();

            _fpsCounterService.OnFpsChanged += ShowFps;
        }

        private void ShowFps(float value)
        {
            _text.text = value.ToStr();
        }
    }
}