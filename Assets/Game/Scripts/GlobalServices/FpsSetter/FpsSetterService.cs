namespace Game.Scripts.GlobalServices.FpsSetter
{
    using Game.Scripts.Attributes;
    using Game.Scripts.DependenciesManagement.Injector;
    using Game.Scripts.GlobalServices.Repository;
    using UnityEngine;

    public class FpsSetterService : InjectableBase, IService
    {
        [Inject] private RepositoryService _repositoryService;

        public FpsSetterService()
        {
            SetTargetFps();
            _repositoryService.GameData.targetFps.OnChanged += SetTargetFps;
        }

        private void SetTargetFps()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = _repositoryService.GameData.targetFps.Value;
        }
    }
}