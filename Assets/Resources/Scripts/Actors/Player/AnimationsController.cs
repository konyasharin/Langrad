using System;
using System.Collections;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Resources.Scripts.Actors.Player
{
    public class AnimationsController
    {
        private static readonly int SpeedAnim = Animator.StringToHash("Speed");
        private static readonly int PrepareMagicAttackHash = Animator.StringToHash("PrepareMagicAttack");
        private static readonly int MagicAttackHash = Animator.StringToHash("MagicAttack");
        private readonly PlayerCharacter _player = ServiceLocator.Instance.Get<PlayerCharacter>();
        private MagicController _magicController;

        public void Initialize(MagicController magicController)
        {
            _magicController = magicController;
        }

        public void UpdateSpeed()
        {
            _player.Animator.SetFloat(SpeedAnim, _player.Rb.velocity.magnitude);
        }

        public void PrepareMagicAttack()
        {
            _player.Animator.SetTrigger(PrepareMagicAttackHash);
        }

        public IEnumerator WaitMagicAttack()
        {
            _magicController.isWaitActivate = true;
            while (_magicController.isWaitActivate)
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
            
            _player.Animator.SetTrigger(MagicAttackHash);
            _magicController.ActivateMagicScroll();
        }
    }
}