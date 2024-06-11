using System;
using Resources.Scripts.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Resources.Scripts.Actors.Player
{
    public class PlayerCharacter : Actor
    {
        public static PlayerCharacter Instance { get; private set; }
        public UnityEvent OnDeath { get; private set; } = new();
        [field: SerializeField, Min(0)] 
        public int Armor { get; protected set; }
        private int _maxHealth;
        [HideInInspector]
        public float speedX;
        [HideInInspector]
        public float speedY;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
            _maxHealth = Health;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    

        private void Update()
        {
            if (!moveIsBlock)
            {
                Move();   
            }
            else
            {
                Rb.velocity = Vector2.zero;
            }
        }

        protected override void Move()
        {
            Rb.velocity = new Vector2(speedX * Speed, speedY * Speed);

            if (speedX < 0)
            {
                SpriteRenderer.flipX = false;
            }
            else if (speedX > 0)
            {
                SpriteRenderer.flipX = true;
            }
        }

        public override void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                if (Armor - damage >= 0)
                {
                    Armor -= damage;
                }
                else
                {
                    damage -= Armor;
                    Health -= damage;
                    Armor = 0;
                }
                OnUpdateStat.Invoke();
            }

            if (Health <= 0)
            {
                Death();
            }
        }

        public void Heal(int healValue)
        {
            if (healValue > 0)
            {
                if (Health + healValue > _maxHealth)
                {
                    Health = _maxHealth;
                }
                else
                {
                    Health += healValue;
                }
                OnUpdateStat.Invoke();
            }
        }

        protected override void Death()
        {
            OnDeath.Invoke();
            Time.timeScale = 0;
        }
    }
}
