using System.Collections;
using Resources.Scripts.Actors.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Bars
{
    [RequireComponent(typeof(Image))]
    public abstract class BarBase : MonoBehaviour
    {
        protected Image Image;
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
        
        private IEnumerator AnimateUpdateValue()
        {
            float currentTime = 0f;
            float newValue = GetValue() / MaxValue;
            while (currentTime < AnimateSpeed)
            {
                Image.fillAmount = Mathf.Lerp(Image.fillAmount, newValue, currentTime / AnimateSpeed);
                currentTime += Time.deltaTime;
                UpdateVisual();
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        protected virtual void UpdateVisual()
        {
            
        }

        protected abstract float GetValue();
    }
}