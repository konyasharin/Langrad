using System;
using System.Collections.Generic;
using Resources.Scripts.Actors.Player;
using Resources.Scripts.InventorySystem;
using Resources.Scripts.UI.Inventory.Slots;
using UnityEngine;

namespace Resources.Scripts.UI.Inventory
{
    public class InventoryDisplay : InventoryDisplayBase<SlotDisplay>
    {
        public static InventoryDisplay Instance { get; private set; }
        protected override SlotDisplay[] SlotsDisplays { get; set; }
        protected override InventoryBase Inventory { get; set; }
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject slots;
        public Canvas Canvas { get; private set; }
        private bool _isInitialized = false;

        private void Awake()
        {
            Instance = this;
            Canvas = GetComponentInParent<Canvas>();
        }

        private void OnDisable()
        {
            if (_isInitialized)
            {
                PlayerCharacter.Instance.moveIsBlock = false;
            }
        }

        private void OnEnable()
        {
            if (_isInitialized)
            {
                PlayerCharacter.Instance.moveIsBlock = true;
            }
        }

        public override void Initialize()
        {
            _isInitialized = true;
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