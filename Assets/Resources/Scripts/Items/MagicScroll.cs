using Resources.Scripts.Actors.Player;
using Resources.Scripts.Bullets;
using Resources.Scripts.Data.ItemsData;
using Resources.Scripts.Spawners;
using UnityEngine;

namespace Resources.Scripts.Items
{
    public class MagicScroll : Item
    {
        private readonly MSData _data;

        public MagicScroll(MSData data)
        {
            _data = data;
        }

        public override void Use()
        {
            PlayerCharacter.Instance.ChangeMana(-_data.energyCosts);
            AnimationsController.Instance.PrepareMagicAttack();
            MagicController.Instance.UpdateMagicScroll(this);
        }

        public void MagicActivate()
        {
            GameObject fireBall = Spawner.Instance.Spawn(_data.magicData.prefab, MagicController.Instance.transform.position);
            fireBall.GetComponent<Magic>().damage = _data.magicData.damage;
        }

        public override bool IsActivationAvailable()
        {
            if (PlayerCharacter.Instance.Mana - _data.energyCosts < 0)
            {
                return false;
            }

            return true;
        }
    }
}