using System.Collections;
using UnityEngine;

namespace Resources.Scripts.ServiceLocatorSystem.Services
{
    public class CoroutinesManager : MonoBehaviour, IService
    {
        public Coroutine StartCoroutineHandle(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }

        public void StopCoroutineHandle(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
    }
}