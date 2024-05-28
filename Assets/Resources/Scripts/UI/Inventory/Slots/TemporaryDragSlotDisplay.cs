using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Inventory.Slots
{
    public class TemporaryDragSlotDisplay : MonoBehaviour
    {
        public static TemporaryDragSlotDisplay Instance;
        private Image _image;
        private RectTransform _rectTransform;

        private void Awake()
        {
            Instance = this;
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
            Hide();
        }

        public void Show(Vector2 position, Sprite sprite)
        {
            gameObject.SetActive(true);
            _rectTransform.anchoredPosition = position;
            _image.sprite = sprite;
        }

        public void ChangeRectTransform(Vector2 delta)
        {
            _rectTransform.anchoredPosition += delta;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}