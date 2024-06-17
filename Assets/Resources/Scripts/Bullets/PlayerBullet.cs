using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Resources.Scripts.Actors.Enemies;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.LevelGenerate;
using Resources.Scripts.LevelGenerate.RoomScripts;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Resources.Scripts.Bullets
{
    public class PlayerBullet : Bullet
    {
        private RoomsManager _roomsManager;

        protected override void Awake()
        {
            base.Awake();
            _roomsManager = ServiceLocator.Instance.Get<RoomsManager>();
        }

        protected override void CalculateVelocity()
        {
            Rb.velocity = GetDirection() * speed;
        }

        protected override void HandleBulletHit(Collision2D other)
        {
            if (other.collider.CompareTag("Enemy"))
            {
                other.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        
        [CanBeNull]
        private Enemy GetNearestEnemy()
        {
            Room activeRoom = _roomsManager.GetActiveRoom();
            if (activeRoom != null && activeRoom.Enemies.Count > 0)
            {
                Enemy nearestEnemy = activeRoom.Enemies[0];
                for (int i = 1; i < activeRoom.Enemies.Count; i++)
                {
                    if (Vector2.Distance(nearestEnemy.transform.position, Player.transform.position) >
                        Vector2.Distance(activeRoom.Enemies[i].transform.position, Player.transform.position) )
                    {
                        nearestEnemy = activeRoom.Enemies[i];
                    }
                }

                return nearestEnemy;
            }

            return null;
        }

        private Vector2 GetDirection()
        {
            Enemy nearestEnemy = GetNearestEnemy();
            if (nearestEnemy != null)
            {
                return nearestEnemy.transform.position - Player.transform.position;
            }
            
            return Player.transform.up;
        }
    }
}
