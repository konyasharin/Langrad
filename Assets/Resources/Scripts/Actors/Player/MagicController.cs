using System;
using Resources.Scripts.Items;
using UnityEngine;

namespace Resources.Scripts.Actors.Player
{
    public class MagicController
    {
        public bool isWaitActivate;
        private MagicScroll _currentMagicScroll;
        private AnimationsController _animationsController;

        public void Initialize(AnimationsController animationsController)
        {
            _animationsController = animationsController;
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
                _animationsController.PrepareMagicAttack();
            }
        }

        public void ActivateMagicScroll()
        {
            _currentMagicScroll.MagicActivate();
            _currentMagicScroll = null;
        }
    }
}