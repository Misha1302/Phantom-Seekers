namespace Game.GameLogic.Scripts
{
    using Game.Scripts.Attributes;
    using Game.Scripts.Helpers;
    using Game.Scripts.Singletons;

    public class InjectField<T>
    {
        [Inject] private T _value;

        private bool _injected;

        public T Value
        {
            get
            {
                if (!_injected)
                    GameSingletons.DependencyInjector.Inject(this);

                _injected = true;

                return _value ?? Thrower.InvalidOpEx($"Value of {typeof(T)} is null").Get<T>();
            }
        }
    }
}