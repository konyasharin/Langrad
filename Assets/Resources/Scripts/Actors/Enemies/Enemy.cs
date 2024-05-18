using System;
using System.Collections;
using Resources.Scripts.Actors.Player;
using UnityEngine;

namespace Resources.Scripts.Actors.Enemies
{
    public abstract class Enemy : Actor
    {
        [field: SerializeField] public int SpawnPrice { get; set; }
        [SerializeField, Min(1)] protected int damage;
        [SerializeField, Min(0.1f)] protected float distanceAttack;
        [SerializeField, Min(0.1f)] protected float cooldownAttack;
        protected bool IsAttack = false;
        protected bool IsCooldown = false;

        protected override void Awake()
        {
            base.Awake();
            moveIsBlock = true;
        }

        private void OnDrawGizmos()
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
    }
}
