using Resources.Scripts.Actors.Player;
using UnityEngine;

namespace Resources.Scripts.Bullets
{
    public class EnemyBullet : Bullet
    {
        protected override void CalculateVelocity()
        {
            Rb.velocity = (PlayerCharacter.Instance.transform.position - transform.position) * speed;
        }

        protected override void HandleBulletHit(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                PlayerCharacter.Instance.TakeDamage(damage);
            }
        }
    }
}
