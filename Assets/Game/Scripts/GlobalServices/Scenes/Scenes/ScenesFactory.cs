namespace Game.Scripts.GlobalServices.Scenes.Scenes
{
    public static class ScenesFactory
    {
        private static readonly AnyScene _anyScene = new();
        private static readonly CoreScene _coreScene = new();

        public static AnyScene AnyScene() => _anyScene;
        public static CoreScene CoreScene() => _coreScene;
    }
}