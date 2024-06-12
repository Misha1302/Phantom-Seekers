namespace Game.Scripts.Bootstrap
{
    using Game.Scripts.Bootstrap.Initialization;
    using Game.Scripts.GlobalServices.Scenes.Scenes;

    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            InitializersManager.InitEveryInitializer(ScenesFactory.AnyScene());
        }
    }
}