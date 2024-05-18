using System.Collections;
using Resources.Scripts.Actors.Player;
using UnityEngine;

namespace Resources.Scripts.Canvas.Bars
{
    public class ArmorBar : Bar
    {
        public static ArmorBar Instance { get; private set; }
        private Coroutine _activeAnimate;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }
        
        public override void Initialize()
        {
            MaxValue = PlayerCharacter.Instance.Armor;
            PlayerCharacter.Instance.OnTakeDamage.AddListener(UpdateValue);
        }
        
        protected override IEnumerator AnimateUpdateValue()
        {
            float currentTime = 0f;
            float newValue = PlayerCharacter.Instance.Armor / MaxValue;
            while (currentTime < AnimateSpeed)
            {
                Image.fillAmount = Mathf.Lerp(Image.fillAmount, newValue, currentTime / AnimateSpeed);
                currentTime += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}
