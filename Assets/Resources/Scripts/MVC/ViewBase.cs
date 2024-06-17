using UnityEngine;

namespace Resources.Scripts.MVC
{
    public abstract class ViewBase<TM, TC> : MonoBehaviour
    {
        [field: SerializeField] protected TM Model { get; private set; }
        protected TC Controller { get; private set; }

        public void InitializeController(TC controller)
        {
            Controller = controller;
        }
    }
}