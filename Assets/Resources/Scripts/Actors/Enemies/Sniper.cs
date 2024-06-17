using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.Bullets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Resources.Scripts.Actors.Enemies
{
    public class Sniper : Enemy
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float distanceRun;
        [SerializeField] private Transform bulletSpawnTransform;
        
        private static readonly int AttackAnimTrigger = Animator.StringToHash("Attack");
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        private readonly Vector2[] _directionsToMove = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

        private void Update()
        {
            if (!IsCooldown && !IsAttack && !moveIsBlock)
            {
                Animator.SetBool(IsRun, false);
                Aim();   
            }
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.yellow;
            foreach (var direction in _directionsToMove)
            {
                Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + direction.x * distanceRun, 
                    transform.position.y + direction.y * distanceRun));
            }
        }

        private void Aim()
        {
            Ray2D ray = new Ray2D(transform.position, Player.transform.position - transform.position);
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
            Bullet bullet = Instantiate(bulletPrefab, bulletSpawnTransform.transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.damage = attackPower;
            IsAttack = false;
            StartCoroutine(Cooldown());
            Animator.SetBool(IsRun, true);
            Move();
        }

        protected override void Move()
        {
            StartCoroutine(WaitStopCooldown(GetMoveDirection()));
        }

        private IEnumerator WaitStopCooldown(Vector2 direction)
        {
            Rb.velocity = new Vector2(direction.x * Speed, direction.y * Speed);
            while (IsCooldown)
            {
                if (Rb.velocity.magnitude == 0)
                {
                    Animator.SetBool(IsRun, false);
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }

            Rb.velocity = Vector2.zero;
            Animator.SetBool(IsRun, false);
        }

        private Vector2 GetMoveDirection()
        {
            List<Vector2> accessDirections = new List<Vector2>();
            foreach (var direction in _directionsToMove)
            {
                Ray2D ray = new Ray2D(transform.position, direction);
                Debug.DrawRay(ray.origin, ray.direction * distanceRun, Color.yellow);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distanceRun, LayerMask.GetMask("Solid Blocks"));
                if (!hit)
                {
                    accessDirections.Add(direction);
                }
            }

            if (accessDirections.Count > 0)
            {
                return accessDirections[Random.Range(0, accessDirections.Count)];
            }
            
            return Vector2.zero;
        }
    }
}
