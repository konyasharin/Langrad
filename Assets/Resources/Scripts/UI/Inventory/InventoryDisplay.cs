using System;
using System.Collections.Generic;
using Resources.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Inventory
{
    public class InventoryDisplay : InventoryDisplayBase
    {
        public static InventoryDisplay Instance { get; private set; }
        protected override SlotDisplay[] SlotsDisplays { get; set; }
        protected override InventoryBase Inventory { get; set; }
        [SerializeField] private GameObject slotPrefab;

        private void Awake()
        {
            Instance = this;
        }

        public override void Initialize()
        {
            List<SlotDisplay> newSlots = new List<SlotDisplay>();
            Inventory = InventorySystem.Inventory.Instance;
            for (int i = 0; i < InventorySystem.Inventory.Instance.CountSlots; i++)
            {
                newSlots.Add(Instantiate(slotPrefab).GetComponent<SlotDisplay>());
                newSlots[^1].transform.SetParent(transform);   
            }

            SlotsDisplays = newSlots.ToArray();
            InitializeHandle();
            gameObject.SetActive(false);
        }
    }
}