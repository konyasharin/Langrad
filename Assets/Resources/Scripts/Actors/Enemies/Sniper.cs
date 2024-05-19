using System;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.Bullets;
using UnityEngine;

namespace Resources.Scripts.Actors.Enemies
{
    public class Sniper : Enemy
    {
        [SerializeField] private GameObject bulletPrefab;
        private static readonly int AttackAnimTrigger = Animator.StringToHash("Attack");

        private void Update()
        {
            if (!IsCooldown && !IsAttack)
            {
                Aim();   
            }
        }

        private void Aim()
        {
            Ray2D ray = new Ray2D(transform.position, PlayerCharacter.Instance.transform.position - transform.position);
            Debug.DrawRay(ray.origin, ray.direction * distanceAttack);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distanceAttack, LayerMask.GetMask("Player"));
            if (hit)
            {
                Animator.SetTrigger(AttackAnimTrigger);
                IsAttack = true;
            }
        }
        
        protected override void Attack()
        {
            Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.damage = damage;
            IsAttack = false;
            StartCoroutine(Cooldown());
        }

        protected override void Move()
        {
            
        }
    }
}
