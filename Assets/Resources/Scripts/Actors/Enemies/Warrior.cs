using System.Collections;
using UnityEngine;
using Resources.Scripts.Actors.Player;

namespace Resources.Scripts.Actors.Enemies
{
    public class Warrior: Enemy
    {
        [Min(0.1f), SerializeField] private float timeWaitAttack;
        
        private static readonly int AnimAttack = Animator.StringToHash("Attack");
        private static readonly int IsRun = Animator.StringToHash("IsRun");

        private void Update()
        {
            if (!moveIsBlock)
            {
                Animator.SetBool(IsRun, true);
                Move();
            }
            else
            {
                Animator.SetBool(IsRun, false);
            }
            
            if (GetDistanceToPlayer() <= distanceAttack && !IsAttack && !IsCooldown)
            {
                StartCoroutine(WaitAttack());
            }
        }

        protected override void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerUtils.GetNearestCorner(transform.position),
                Speed * Time.deltaTime);
        }

        private IEnumerator WaitAttack()
        {
            IsAttack = true;
            yield return new WaitForSeconds(timeWaitAttack);
            Attack();
        }

        private IEnumerator WaitCooldown()
        {
            yield return WaitAnimationEnd();
            yield return Cooldown();
        }

        protected override void Attack()
        {
            Animator.SetTrigger(AnimAttack);
            if (GetDistanceToPlayer() <= distanceAttack)
            {
                Player.TakeDamage(attackPower);
            }

            StartCoroutine(WaitCooldown());
        }
    }
}
