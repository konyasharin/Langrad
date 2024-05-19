using System;
using System.Collections;
using Resources.Scripts.Actors.Player;
using UnityEngine;

namespace Resources.Scripts.Bullets
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), 
        typeof(Animator))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        protected float speed;
        [HideInInspector]
        public int damage;

        private Rigidbody2D _rb;
        private Animator _animator;
        private static readonly int DestroyAnimTrigger = Animator.StringToHash("Destroy");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _rb.velocity = (PlayerCharacter.Instance.transform.position - transform.position) * speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                PlayerCharacter.Instance.TakeDamage(damage);
            }
            _animator.SetTrigger(DestroyAnimTrigger);
        }

        public void DestroyAfterAnim()
        {
            Destroy(gameObject);
        }
    }
}
