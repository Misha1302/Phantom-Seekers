namespace Game.Scripts.Extensions.Math.Numbers
{
    using Game.Scripts.GlobalServices.Input.Axis;

    public static class AxisExtensions
    {
        public static int Sign(this Axis axis) => ((float)axis).Sign();
    }
}