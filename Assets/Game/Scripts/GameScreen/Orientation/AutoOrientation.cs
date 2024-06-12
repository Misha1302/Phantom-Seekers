namespace Game.Scripts.GameScreen.Orientation
{
    using System;

    // ReSharper disable UnusedMember.Global
    [Flags]
    public enum AutoOrientation
    {
        Invalid = 0,
        LandscapeLeft = 1 << 0,
        LandscapeRight = 1 << 1,
        PortraitUp = 1 << 2,
        PortraitDown = 1 << 3,

        Landscape = LandscapeLeft | LandscapeRight,
        Portrait = PortraitUp | PortraitDown
    }
}