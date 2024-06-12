namespace Game.Scripts.GameScreen.Orientation
{
    public static class AutoOrientationExtensions
    {
        public static bool HasFlagFast(this AutoOrientation value, AutoOrientation flag) =>
            (value & flag) != 0;
    }
}