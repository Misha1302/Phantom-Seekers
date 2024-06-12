namespace Game.Scripts.Extensions
{
    public static class FormatExtensions
    {
        public static string ToStr(this float value, string format = "0.###") =>
            value.ToString(format);
    }
}