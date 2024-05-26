using System;
using Resources.Scripts.Items;
using UnityEngine;

namespace Resources.Scripts.Actors.Player
{
    public class MagicController : MonoBehaviour
    {
        public static MagicController Instance;
        private MagicScroll _currentMagicScroll;
        [HideInInspector]
        public bool isWaitActivate;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && isWaitActivate)
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