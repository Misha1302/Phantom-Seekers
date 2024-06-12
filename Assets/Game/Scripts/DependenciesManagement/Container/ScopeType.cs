namespace Game.Scripts.DependenciesManagement.Container
{
    using System;

    public class ScopeType
    {
        private readonly Type _types;

        public ScopeType(Type types)
        {
            _types = types;
        }

        public override bool Equals(object obj) => obj is ScopeType types && _types == types._types;
        public override int GetHashCode() => _types != null ? _types.GetHashCode() : 0;
        public override string ToString() => _types.ToString();


        public static implicit operator ScopeType(Type types) => new(types);
        public static implicit operator Type(ScopeType scopeType) => scopeType._types;
    }
}