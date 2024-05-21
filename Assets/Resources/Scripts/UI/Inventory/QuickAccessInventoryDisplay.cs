using System;
using Resources.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Inventory
{
    public class QuickAccessInventoryDisplay : MonoBehaviour
    {
        public static QuickAccessInventoryDisplay Instance;
        [SerializeField] private Image[] itemPlaces;

        private void Awake()
        {
            Instance = this;
        }

        public void Initialize()
        {
            if (itemPlaces.Length != QuickAccessInventory.Instance.CountSlots)
            {
                Debug.LogWarning("Count of elements in itemPlaces array not equal" +
                                 " count slots in quick access inventory");
            }
            QuickAccessInventory.Instance.OnChangeInventory.AddListener(UpdateInventoryDisplay);
        }

        private void UpdateInventoryDisplay()
        {
            for (int i = 0; i < itemPlaces.Length; i++)
            {
                if (QuickAccessInventory.Instance.Slots[i].IsBusy())
                {
                    itemPlaces[i].color = Color.white;
                    itemPlaces[i].sprite = QuickAccessInventory.Instance.Slots[i].item.SpriteRenderer.sprite;
                }
                else
                {
                    itemPlaces[i].color = Color.clear;
                    itemPlaces[i].sprite = null;
                }
            }
        }
    }
}
