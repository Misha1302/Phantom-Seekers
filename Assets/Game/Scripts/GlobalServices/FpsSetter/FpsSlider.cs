namespace Game.Scripts.GlobalServices.FpsSetter
{
    using Game.Scripts.Attributes;
    using Game.Scripts.Extensions;
    using Game.Scripts.GlobalServices.GameManager.MonoBeh;
    using Game.Scripts.GlobalServices.Repository;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Slider))]
    public class FpsSlider : MonoBeh
    {
        [Inject] private RepositoryService _repositoryService;

        private Slider _slider;

        protected override void OnStart()
        {
            _slider = GetComponent<Slider>();
            _slider.wholeNumbers = true;
            _slider.onValueChanged.AddListener(SetFps);

            _slider.value = _repositoryService.GameData.targetFps.Value
                .ThisOrIf(x => x < 0, (int)_slider.maxValue);
        }

        private void SetFps(float sliderValue)
        {
            _repositoryService.GameData.targetFps.Value =
                (int)sliderValue != (int)_slider.maxValue ? (int)sliderValue : -1;
        }
    }
}