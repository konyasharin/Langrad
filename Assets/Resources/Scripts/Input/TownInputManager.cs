using System;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.Data;
using Resources.Scripts.DialogSystem;
using Resources.Scripts.Entities;
using Resources.Scripts.ServiceLocatorSystem;
using Resources.Scripts.UI;
using UnityEngine;

namespace Resources.Scripts.Input
{
    public class TownInputManager : MonoBehaviour, IService
    {
        [SerializeField] protected InputData inputData;
        
        private PlayerCharacter _player;
        private InteractionManager _interactionManager;
        private DialogWindow _dialogWindow;

        public virtual void Initialize()
        {
            _player = ServiceLocator.Instance.Get<PlayerCharacter>();
            _interactionManager = ServiceLocator.Instance.Get<InteractionManager>();
            _dialogWindow = ServiceLocator.Instance.Get<DialogsManager>().DialogWindow;
        }

        protected virtual void Update()
        {
            CheckSkipDialog();
            CheckInteract();
            UpdatePlayerSpeed();
            CheckActivatePause();
        }

        private void CheckSkipDialog()
        {
            if (UnityEngine.Input.GetKeyDown(inputData.skipDialogKey))
            {
                _dialogWindow.HandleKeyDown();
            }
        }

        private void CheckInteract()
        {
            if (UnityEngine.Input.GetKeyDown(inputData.entityInteractKey))
            {
                _interactionManager.HandleKeyDown();
            }
        }

        private void UpdatePlayerSpeed()
        {
            _player.speedX = UnityEngine.Input.GetAxis("Horizontal");
            _player.speedY = UnityEngine.Input.GetAxis("Vertical");
        }

        private void CheckActivatePause()
        {
            if (UnityEngine.Input.GetKeyDown(inputData.pauseActivateKey))
            {
                PauseMenu.Instance.gameObject.SetActive(!PauseMenu.Instance.gameObject.activeSelf);
            }
        }
    }
}