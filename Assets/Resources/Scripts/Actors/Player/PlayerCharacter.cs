using System;
using UnityEngine;
using UnityEngine.Events;

namespace Resources.Scripts.Actors.Player
{
    public class PlayerCharacter : Actor
    {
        private static readonly int IsRun = Animator.StringToHash("isRun");
        public static PlayerCharacter Instance { get; private set; }
        public UnityEvent OnTakeDamage { get; private set; } = new();
        public UnityEvent OnDeath { get; private set; } = new();
    
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }

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
            else
            {
                Animator.SetBool(IsRun, false);
            }
        }

        protected override void Move()
        {
            float speedX = Input.GetAxis("Horizontal");
            float speedY = Input.GetAxis("Vertical");
            Rb.velocity = new Vector2(speedX * Speed, speedY * Speed);
            if (Math.Abs(speedX) > 0 || Math.Abs(speedY) > 0)
            {
                Animator.SetBool(IsRun, true);
            }
            else
            {
                Animator.SetBool(IsRun, false);
            }

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
