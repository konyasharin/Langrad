using System.Collections;
using Resources.Scripts.Actors.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Bars
{
    [RequireComponent(typeof(Image))]
    public abstract class Bar : MonoBehaviour
    {
        private Image _image;
        private float _maxValue;
        [field: SerializeField, Min(0.1f)]
        protected float AnimateSpeed { get; private set; }
        private Coroutine _activeAnimate;

        protected virtual void Awake()
        {
            _image = GetComponent<Image>();
            _image.fillAmount = 1;
        }

        private void UpdateValue()
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
            float newValue = GetValue() / _maxValue;
            while (currentTime < AnimateSpeed)
            {
                _image.fillAmount = Mathf.Lerp(_image.fillAmount, newValue, currentTime / AnimateSpeed);
                currentTime += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        
        public void Initialize()
        {
            _maxValue = GetValue();
            PlayerCharacter.Instance.OnUpdateStat.AddListener(UpdateValue);
        }

        protected abstract float GetValue();
    }
}
