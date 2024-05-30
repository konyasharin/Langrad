using System;
using System.Collections;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.LevelGenerate;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Resources.Scripts.Actors.Enemies
{
    public abstract class Enemy : Actor
    {
        [field: SerializeField] public int SpawnPrice { get; set; }
        [SerializeField, Min(1)] protected int attackPower;
        [SerializeField, Min(0.1f)] protected float distanceAttack;
        [SerializeField, Min(0.1f)] protected float cooldownAttack;
        protected bool IsAttack = false;
        protected bool IsCooldown = false;
        protected UnityEvent OnEndCooldown = new();
        public UnityEvent<Enemy> OnDeath { get; private set; } = new();

        protected override void Awake()
        {
            base.Awake();
            moveIsBlock = true;
        }

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanceAttack);
        }

        protected float GetDistanceToPlayer()
        {
            return Vector2.Distance(PlayerCharacter.Instance.transform.position, transform.position);
        }

        protected abstract void Attack();

        protected IEnumerator Cooldown()
        {
            IsAttack = false;
            IsCooldown = true;
            yield return new WaitForSeconds(cooldownAttack);
            IsCooldown = false;
        }

        protected IEnumerator WaitAnimationEnd()
        {
            yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length);
        }

        public override void TakeDamage(int damage)
        {
            if (Health - damage <= 0)
            {
                Health = 0;
            }
            else
            {
                Health -= damage;
            }

            if (Health <= 0)
            {
                Death();
            }
            OnUpdateStat.Invoke();
        }

        protected override void Death()
        {
            OnDeath.Invoke(this);
            Destroy(gameObject);
        }
    }
}
