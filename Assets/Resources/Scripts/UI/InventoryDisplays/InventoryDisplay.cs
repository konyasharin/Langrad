using System.Collections.Generic;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.InventorySystem;
using Resources.Scripts.ServiceLocatorSystem;
using Resources.Scripts.UI.InventoryDisplays.Slots;
using UnityEngine;

namespace Resources.Scripts.UI.InventoryDisplays
{
    public class InventoryDisplay : InventoryDisplayBase<SlotDisplay, Inventory>
    {
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject slots;
        
        public Canvas Canvas { get; private set; }
        
        private bool _isInitialized = false;
        private PlayerCharacter _player;

        private void Awake()
        {
            Canvas = GetComponentInParent<Canvas>();
        }

        private void OnDisable()
        {
            if (_isInitialized)
            {
                _player.moveIsBlock = false;
            }
        }

        private void OnEnable()
        {
            if (_isInitialized)
            {
                _player.moveIsBlock = true;
            }
        }

        public void Initialize()
        {
            _isInitialized = true;
            _player = ServiceLocator.Instance.Get<PlayerCharacter>();
            Inventory = ServiceLocator.Instance.Get<InventoryManager>().Inventory;
            
            List<SlotDisplay> newSlots = new List<SlotDisplay>();
            for (int i = 0; i < Inventory.CountSlots; i++)
            {
                newSlots.Add(Instantiate(slotPrefab, slots.transform).GetComponent<SlotDisplay>());
            }

            SlotsDisplays = newSlots.ToArray();
            InitializeHandle();
            
            gameObject.SetActive(false);
        }
    }
}