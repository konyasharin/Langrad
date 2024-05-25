using Resources.Scripts.Actors.Player;
using Resources.Scripts.Bullets.Spells;
using Resources.Scripts.ItemsData;
using Resources.Scripts.ItemsData.MagicScrolls;
using Resources.Scripts.Spawners;
using UnityEngine;

namespace Resources.Scripts.Items.MagicScrolls
{
    public class MSFireBall: MagicScroll
    {
        private readonly MSFireBallData _data;
        
        public MSFireBall(MSFireBallData data)
        {
            _data = data;
        }

        public override void Use()
        {
            PlayerCharacter.Instance.ChangeMana(-_data.energyCosts);
            GameObject fireBall = Spawner.Instance.Spawn(_data.prefab, PlayerCharacter.Instance.transform.position);
            fireBall.GetComponent<FireBall>().damage = _data.damage;
        }
    }
}