namespace Game.Scripts.GlobalServices.Coroutines.Waitings
{
    public static class WaitingsFactory
    {
        private static readonly WaitNextFixed _waitNextFixed = new();
        private static readonly WaitNextFrame _waitNextFrame = new();

        public static WaitSeconds WaitSeconds(float seconds) => new(seconds);
        public static WaitNextFixed WaitNextFixed() => _waitNextFixed;
        public static WaitNextFrame WaitNextFrame() => _waitNextFrame;
    }
}