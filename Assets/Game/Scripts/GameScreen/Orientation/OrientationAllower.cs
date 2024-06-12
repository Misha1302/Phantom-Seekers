namespace Game.Scripts.GameScreen.Orientation
{
    using UnityEngine;

    public static class OrientationAllower
    {
        public static void AllowOrientation(AutoOrientation orientation, AutoOrientation prefer)
        {
            SetAutorotation(orientation);
            SetPreferred(prefer);
        }

        private static void SetAutorotation(AutoOrientation orientation)
        {
            Screen.autorotateToLandscapeLeft = orientation.HasFlagFast(AutoOrientation.LandscapeLeft);
            Screen.autorotateToLandscapeLeft = orientation.HasFlagFast(AutoOrientation.LandscapeRight);
            Screen.autorotateToPortrait = orientation.HasFlagFast(AutoOrientation.PortraitUp);
            Screen.autorotateToPortraitUpsideDown = orientation.HasFlagFast(AutoOrientation.PortraitDown);
        }

        private static void SetPreferred(AutoOrientation prefer)
        {
            if (prefer.HasFlagFast(AutoOrientation.LandscapeLeft))
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            else if (prefer.HasFlagFast(AutoOrientation.LandscapeRight))
                Screen.orientation = ScreenOrientation.LandscapeRight;
            else if (prefer.HasFlagFast(AutoOrientation.PortraitUp))
                Screen.orientation = ScreenOrientation.Portrait;
            else if (prefer.HasFlagFast(AutoOrientation.PortraitDown))
                Screen.orientation = ScreenOrientation.PortraitUpsideDown;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}