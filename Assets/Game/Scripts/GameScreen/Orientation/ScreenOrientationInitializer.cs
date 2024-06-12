namespace Game.Scripts.GameScreen.Orientation
{
    using Game.Scripts.Attributes;
    using Game.Scripts.Bootstrap.Initialization;
    using Game.Scripts.GlobalServices.Scenes.Scenes;

    public class ScreenOrientationInitializer
    {
        [SceneInitializer(typeof(AnyScene), initializationType: InitializationType.Once)]
        public static void Initialize()
        {
            OrientationAllower.AllowOrientation(AutoOrientation.Landscape, AutoOrientation.LandscapeRight);
        }
    }
}