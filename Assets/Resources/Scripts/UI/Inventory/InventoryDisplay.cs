using System;
using System.Collections.Generic;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.InventorySystem;
using Resources.Scripts.ServiceLocatorSystem;
using Resources.Scripts.UI.Inventory.Slots;
using UnityEngine;

namespace Resources.Scripts.UI.Inventory
{
    public class InventoryDisplay : InventoryDisplayBase<SlotDisplay>
    {
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject slots;
        public Canvas Canvas { get; private set; }
        protected override SlotDisplay[] SlotsDisplays { get; set; }
        protected override InventoryBase Inventory { get; set; }
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

        public override void Initialize()
        {
            _isInitialized = true;
            _player = ServiceLocator.Instance.Get<PlayerCharacter>();
            List<SlotDisplay> newSlots = new List<SlotDisplay>();
            Inventory = InventorySystem.Inventory.Instance;
            for (int i = 0; i < InventorySystem.Inventory.Instance.CountSlots; i++)
            {
                newSlots.Add(Instantiate(slotPrefab, slots.transform).GetComponent<SlotDisplay>());
            }

            SlotsDisplays = newSlots.ToArray();
            InitializeHandle();
            gameObject.SetActive(false);
        }
    }
}