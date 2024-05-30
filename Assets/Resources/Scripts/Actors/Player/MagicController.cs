using System;
using Resources.Scripts.Items;
using UnityEngine;

namespace Resources.Scripts.Actors.Player
{
    public class MagicController : MonoBehaviour
    {
        public static MagicController Instance { get; private set; }
        private MagicScroll _currentMagicScroll;
        [HideInInspector]
        public bool isWaitActivate;

        private void Awake()
        {
            Instance = this;
        }

        public void HandleKeyDown()
        {
            if (isWaitActivate)
            {
                isWaitActivate = false;
            }
        }

        public void UpdateMagicScroll(MagicScroll magicScroll)
        {
            if (_currentMagicScroll == null)
            {
                _currentMagicScroll = magicScroll;
                AnimationsController.Instance.PrepareMagicAttack();
            }
        }

        public void ActivateMagicScroll()
        {
            _currentMagicScroll.MagicActivate();
            _currentMagicScroll = null;
        }
    }
}