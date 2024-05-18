using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.Canvas.Bars
{
    [RequireComponent(typeof(Image))]
    public abstract class Bar : MonoBehaviour
    {
        protected Image Image { get; private set; }
        protected float MaxValue;
        [field: SerializeField, Min(0.1f)]
        protected float AnimateSpeed { get; private set; }
        private Coroutine _activeAnimate;

        protected virtual void Awake()
        {
            Image = GetComponent<Image>();
            Image.fillAmount = 1;
        }

        protected void UpdateValue()
        {
            if (_activeAnimate != null)
            {
                StopCoroutine(_activeAnimate);
            }

            _activeAnimate = StartCoroutine(AnimateUpdateValue());
        }
        
        protected abstract IEnumerator AnimateUpdateValue();
        public abstract void Initialize();
    }
}
