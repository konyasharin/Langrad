using System;
using System.Collections;
using UnityEngine;

namespace Resources.Scripts.Actors.Player
{
    public class AnimationsController : MonoBehaviour
    {
        public static AnimationsController Instance { get; private set; }
        private static readonly int SpeedAnim = Animator.StringToHash("Speed");
        private static readonly int PrepareMagicAttackHash = Animator.StringToHash("PrepareMagicAttack");
        private static readonly int MagicAttackHash = Animator.StringToHash("MagicAttack");
        private PlayerCharacter _playerCharacter;

        private void Start()
        {
            _playerCharacter = PlayerCharacter.Instance;
            Instance = this;
        }

        private void Update()
        {
            SetSpeed(_playerCharacter.Rb.velocity.magnitude);
        }

        private void SetSpeed(float speed)
        {
            _playerCharacter.Animator.SetFloat(SpeedAnim, speed);
        }

        public void PrepareMagicAttack()
        {
            _playerCharacter.Animator.SetTrigger(PrepareMagicAttackHash);
        }

        public IEnumerator WaitMagicAttack()
        {
            MagicController.Instance.isWaitActivate = true;
            while (MagicController.Instance.isWaitActivate)
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
            
            _playerCharacter.Animator.SetTrigger(MagicAttackHash);
            MagicController.Instance.ActivateMagicScroll();
        }
    }
}