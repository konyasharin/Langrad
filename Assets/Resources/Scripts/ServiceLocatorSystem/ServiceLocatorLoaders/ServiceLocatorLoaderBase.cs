using UnityEngine;

namespace Resources.Scripts.ServiceLocatorSystem.ServiceLocatorLoaders
{
    public abstract class ServiceLocatorLoaderBase : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator.Initialize();
            
            AddServices();
            InitializeServices();
        }

        protected abstract void AddServices();
        protected abstract void InitializeServices();
    }
}