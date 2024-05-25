using System.Collections;
using Resources.Scripts.Actors.Player;
using UnityEngine;

namespace Resources.Scripts.UI.Bars
{
    public class HealthBar : Bar
    {
        public static HealthBar Instance { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        protected override float GetValue()
        {
            return PlayerCharacter.Instance.Health;
        }
    }
}
