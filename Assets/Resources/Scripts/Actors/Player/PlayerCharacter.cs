using System;
using Resources.Scripts.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Resources.Scripts.Actors.Player
{
    public class PlayerCharacter : Actor
    {
        private static readonly int SpeedAnim = Animator.StringToHash("Speed");
        public static PlayerCharacter Instance { get; private set; }
        public UnityEvent OnTakeDamage { get; private set; } = new();
        public UnityEvent OnDeath { get; private set; } = new();

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
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
            Animator.SetFloat(SpeedAnim, Rb.velocity.magnitude);
        }

        protected override void Move()
        {
            float speedX = Input.GetAxis("Horizontal");
            float speedY = Input.GetAxis("Vertical");
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

        public void TakeDamage(int damage)
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
                OnTakeDamage.Invoke();
            }

            if (Health <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            OnDeath.Invoke();
            Time.timeScale = 0;
        }
    }
}
