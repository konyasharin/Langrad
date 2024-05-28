using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Inventory
{
    public class SlotDisplay : MonoBehaviour
    {
        [field: SerializeField]
        public Image ItemPlace { get; private set; }

        public void Clear()
        {
            ItemPlace.color = Color.clear;
            ItemPlace.sprite = null;
        }

        public void Fill(Sprite newSprite)
        {
            ItemPlace.color = Color.white;
            ItemPlace.sprite = newSprite;
        }
    }
}