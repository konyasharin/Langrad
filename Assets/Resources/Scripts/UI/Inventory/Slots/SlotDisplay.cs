using System;
using Resources.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Inventory.Slots
{
    public class SlotDisplay : SlotDisplayBase, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private TemporaryDragSlotDisplay _dragSlot;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _dragSlot = TemporaryDragSlotDisplay.Instance;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragSlot.Show(_rectTransform.anchoredPosition, ItemPlace.sprite);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragSlot.ChangeRectTransform(eventData.delta / InventoryDisplay.Instance.Canvas.scaleFactor);
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            _dragSlot.Hide();
        }
    }
}