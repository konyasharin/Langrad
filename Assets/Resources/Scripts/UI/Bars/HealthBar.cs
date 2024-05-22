using System.Collections;
using Resources.Scripts.Actors.Player;
using UnityEngine;

namespace Resources.Scripts.UI.Bars
{
    public class HealthBar : Bar
    {
        public static HealthBar Instance { get; private set; }
        private Coroutine _activeAnimate;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }
        
        public override void Initialize()
        {
            MaxValue = PlayerCharacter.Instance.Health;
            PlayerCharacter.Instance.OnUpdateHealth.AddListener(UpdateValue);
        }

        protected override IEnumerator AnimateUpdateValue()
        {
            float currentTime = 0f;
            float newValue = PlayerCharacter.Instance.Health / MaxValue;
            while (currentTime < AnimateSpeed)
            {
                Image.fillAmount = Mathf.Lerp(Image.fillAmount, newValue, currentTime / AnimateSpeed);
                currentTime += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}
