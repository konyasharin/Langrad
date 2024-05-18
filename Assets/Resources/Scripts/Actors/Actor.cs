using System;
using UnityEngine;

namespace Resources.Scripts.Actors
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public abstract class Actor : MonoBehaviour
    {
        [SerializeField] 
        protected int health;
        [field: Min(0.1f), SerializeField]
        public float Speed { get; protected set; }
        protected Animator Animator;
        protected SpriteRenderer SpriteRenderer;
        [HideInInspector] 
        public bool moveIsBlock = false;

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected abstract void Move();
    }
}
