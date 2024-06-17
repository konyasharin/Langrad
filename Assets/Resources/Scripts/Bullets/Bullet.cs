using System;
using System.Collections;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Resources.Scripts.Bullets
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), 
        typeof(Animator))]
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [HideInInspector] public int damage;
            
        protected Rigidbody2D Rb;
        private Animator _animator;
        protected PlayerCharacter Player;
        private static readonly int DestroyAnimTrigger = Animator.StringToHash("Destroy");

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            Player = ServiceLocator.Instance.Get<PlayerCharacter>();
        }

        private void Start()
        {
            CalculateVelocity();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            HandleBulletHit(other);
            Rb.velocity = Vector2.zero;
            _animator.SetTrigger(DestroyAnimTrigger);
        }

        public void DestroyAfterAnim()
        {
            Destroy(gameObject);
        }

        protected abstract void CalculateVelocity();
        protected abstract void HandleBulletHit(Collision2D other);
    }
}
