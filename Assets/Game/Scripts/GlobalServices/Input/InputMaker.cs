namespace Game.Scripts.GlobalServices.Input
{
    using Game.Scripts.GlobalServices.Creator;
    using Game.Scripts.GlobalServices.Input.Services;
    using UnityEngine;

    public static class InputMaker
    {
        public static IInputService MakeInputService(CreatorService creatorService)
        {
            if (Input.touchSupported || RunMode.IsEditorSimulator())
                return creatorService.Create<MobileInputService>();
            return creatorService.Create<PcInputService>();
        }
    }
}