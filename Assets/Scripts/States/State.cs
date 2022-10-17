using UnityEngine;

namespace TMD
{
    public abstract class State
    {
        protected bool isStateChanged;
        public virtual void Enter()
        {
            isStateChanged = false;
        }
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void LateUpdate();
        public virtual void Exit()
        {
            isStateChanged = true;
        }
        protected bool IsStateChanged()
        {
            if (isStateChanged)
            {
                Debug.LogWarning("IsStateChanged: " + isStateChanged +
                    ". Changed from state " + this.GetType().Name +
                    " in the middle of Update/... function");
            }
            return isStateChanged;
        }

        public static bool IsAssignableFromState<T>(State checkState)
        {
            return typeof(T).IsAssignableFrom(checkState.GetType());
        }
    }
}
