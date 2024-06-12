namespace Game.Scripts.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Game.Scripts.DependenciesManagement.Container;

    public static class Thrower
    {
        [DoesNotReturn]
        public static Any InvalidOpEx(string msg) => throw new InvalidOperationException(msg);
    }
}