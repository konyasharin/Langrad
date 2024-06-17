using Resources.Scripts.Actors.Player;
using Resources.Scripts.Actors.Player.ManaSystem;
using Resources.Scripts.Bullets;
using Resources.Scripts.Data.ItemsData;
using Resources.Scripts.ServiceLocatorSystem;
using Resources.Scripts.Spawners;
using UnityEngine;

namespace Resources.Scripts.Items
{
    public class MagicScroll : Item
    {
        private readonly MSData _data;
        private readonly Spawner _spawner = ServiceLocator.Instance.Get<Spawner>();
        private readonly PlayerCharacter _player = ServiceLocator.Instance.Get<PlayerCharacter>();

        public MagicScroll(MSData data)
        {
            _data = data;
        }

        public override void Use()
        {
            _player.ManaController.Change(-_data.energyCosts);
            _player.AnimationsController.PrepareMagicAttack();
            _player.MagicController.UpdateMagicScroll(this);
        }

        public void MagicActivate()
        {
            GameObject fireBall = _spawner.Spawn(_data.magicData.prefab, _player.transform.position);
            fireBall.GetComponent<Magic>().damage = _data.magicData.damage;
        }

        public override bool IsActivationAvailable()
        {
            if (_player.ManaController.Mana - _data.energyCosts < 0)
            {
                return false;
            }

            return true;
        }
    }
}