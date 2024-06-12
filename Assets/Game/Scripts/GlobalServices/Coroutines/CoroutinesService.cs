namespace Game.Scripts.GlobalServices.Coroutines
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Game.Scripts.Extensions;
    using Game.Scripts.GlobalServices.Coroutines.Waitings;
    using Game.Scripts.GlobalServices.GameManager.MonoBeh;
    using JetBrains.Annotations;

    public class CoroutinesService : MonoBeh, IService
    {
        private readonly List<(string name, List<CoroutineObj> cors)> _coroutines = new();

        public void StartCor(
            IEnumerator<ICoroutineWaiting> enumerator,
            string corName = "",
            [CanBeNull] Action whenCoroutineEnd = null)
        {
            var cor = new CoroutineObj(enumerator, whenCoroutineEnd);
            GetCors(corName).Add(cor);
            cor.OnEnd += () => RemoveCoroutine(cor);
        }

        private void RemoveCoroutine(CoroutineObj coroutineObj)
        {
            _coroutines.ForAll(x => x.cors.Remove(coroutineObj));
        }

        public void StopCorsGroup(string corsGroup)
        {
            _coroutines.FirstOrDefault(x => x.name == corsGroup).cors.ForAll(x => x.Stop());
        }

        protected override void Tick()
        {
            _coroutines.ForAll(x => x.cors.ForAll(y => y.Tick()));
        }

        protected override void FixedTick()
        {
            _coroutines.ForAll(x => x.cors.ForAll(y => y.FixedTick()));
        }

        private List<CoroutineObj> GetCors(string corName)
        {
            var value = _coroutines.FirstOrDefault(x => x.name == corName).cors;
            if (value == null)
                _coroutines.Add((corName, value = new List<CoroutineObj>()));

            return value;
        }
    }
}