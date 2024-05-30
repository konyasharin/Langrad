using System;
using UnityEngine;
using UnityEngine.Events;

namespace Resources.Scripts.Actors
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Actor : MonoBehaviour
    {
        [field: SerializeField, Min(1)] 
        public int Health { get; protected set; }
        [field: Min(0.1f), SerializeField]
        public float Speed { get; protected set; }
        public Animator Animator { get; private set; }
        protected SpriteRenderer SpriteRenderer;
        public Collider2D Collider { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        [HideInInspector] 
        public bool moveIsBlock = false;
        public UnityEvent OnUpdateStat { get; private set; } = new();

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Collider = GetComponent<Collider2D>();
            Rb = GetComponent<Rigidbody2D>();
        }

        protected abstract void Move();
        public abstract void TakeDamage(int damage);
        protected abstract void Death();
    }
}
