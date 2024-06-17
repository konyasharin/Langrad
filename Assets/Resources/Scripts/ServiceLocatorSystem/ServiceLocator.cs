using System;
using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts.ServiceLocatorSystem
{
    public class ServiceLocator
    {
        private readonly Dictionary<string, IService> _services = new();
        public static ServiceLocator Instance { get; private set; }

        public static void Initialize()
        {
            Instance = new ServiceLocator();
        }

        public void Add<T>(T service) where T : IService
        {
            if (!_services.TryAdd(typeof(T).Name, service))
            {
                Debug.LogError($"Service {typeof(T).Name} already exist");
            }
        }

        public T Get<T>() where T : IService
        {
            if (_services.ContainsKey(typeof(T).Name))
            {
                return (T)_services[typeof(T).Name];
            }
            Debug.LogError($"Service {typeof(T).Name} doesn't exist");
            throw new Exception();
        }
    }
}