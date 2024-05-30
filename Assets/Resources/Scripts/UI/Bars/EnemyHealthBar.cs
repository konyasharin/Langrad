using System.Collections.Generic;
using Resources.Scripts.Actors.Enemies;
using UnityEngine;

namespace Resources.Scripts.UI.Bars
{
    public class EnemyHealthBar : BarBase
    {
        [SerializeField]
        private Enemy enemy;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        protected override float GetValue()
        {
            return enemy.Health;
        }

        protected override void UpdateVisual()
        {
            base.UpdateVisual();
            float currentValue = GetValue();
            if (currentValue / MaxValue >= 0.8f)
            {
                Image.color = Color.green;
            } else if (currentValue / MaxValue >= 0.35f)
            {
                Image.color = Color.yellow;
            }
            else
            {
                Image.color = Color.red;
            }
        }

        private void Initialize()
        {
            MaxValue = GetValue();
            enemy.OnUpdateStat.AddListener(UpdateValue);
            UpdateVisual();
        }
    }
}