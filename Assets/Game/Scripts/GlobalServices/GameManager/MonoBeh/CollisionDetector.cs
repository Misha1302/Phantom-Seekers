namespace Game.Scripts.GlobalServices.GameManager.MonoBeh
{
    using System;
    using UnityEngine;

    public class CollisionDetector : MonoBeh
    {
        public Action<Collision2D> OnColEnter2D;
        public Action<Collision2D> OnColExit2D;
        public Action<Collision2D> OnColStay2D;

        public Action<Collider2D> OnTrigEnter2D;
        public Action<Collider2D> OnTrigExit2D;
        public Action<Collider2D> OnTrigStay2D;

        public virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (isEnabled) OnColEnter2D?.Invoke(other);
        }

        public virtual void OnCollisionExit2D(Collision2D other)
        {
            if (isEnabled) OnColExit2D?.Invoke(other);
        }

        public virtual void OnCollisionStay2D(Collision2D other)
        {
            if (isEnabled) OnColStay2D?.Invoke(other);
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (isEnabled) OnTrigEnter2D?.Invoke(other);
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            if (isEnabled) OnTrigExit2D?.Invoke(other);
        }

        public virtual void OnTriggerStay2D(Collider2D other)
        {
            if (isEnabled) OnTrigStay2D?.Invoke(other);
        }
    }
}