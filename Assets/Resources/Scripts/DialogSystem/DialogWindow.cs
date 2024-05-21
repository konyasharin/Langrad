using System.Collections;
using Resources.Scripts.UI;
using TMPro;
using UnityEngine;

namespace Resources.Scripts.DialogSystem
{
    public class DialogWindow : MonoBehaviour
    {
        public static DialogWindow Instance { get; private set; }
        [SerializeField] private TMP_Text tmpTextField;
        [SerializeField] private TMP_Text tmpNameField;
        [SerializeField] private CanvasMove move;
        private RectTransform _rectTransform;
        /// <summary>
        /// Показывает активно ли сейчас диалоговое окно
        /// </summary>
        private bool _isActive = false;
        /// <summary>
        /// Показывает пропустил ли пользователь текущий текст
        /// </summary>
        private bool _isSkip = false;
        /// <summary>
        /// Показывает ожидается ли клик пользователя для продолжения диалога
        /// (или для прекращения диалога)
        /// </summary>
        private bool _isWait = false;
        [Range(0.05f, 0.1f)]
        [SerializeField]
        private float speedText;
    
        private void Awake()
        {
            Instance = this;
        }
    
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_isActive && Input.GetKeyDown(KeyCode.Mouse0) && !_isSkip)
            {
                _isSkip = true;
            }

            if (_isActive && _isWait && Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isWait = false;
            }
        }

        public IEnumerator Activate()
        {
            tmpNameField.text = "";
            tmpTextField.text = "";
            yield return StartCoroutine(CanvasManager.Move(move, _rectTransform));
            _isActive = true;
        }

        public IEnumerator Deactivate()
        {
            _isActive = false;
            _isSkip = false;
            _isWait = false;
            CanvasMove deactivateMove = move;
            deactivateMove.fromPosition = move.toPosition;
            deactivateMove.toPosition = move.fromPosition;
            yield return StartCoroutine(CanvasManager.Move(deactivateMove, _rectTransform));
        }

        public IEnumerator ShowText(Sentence sentence)
        {
            tmpNameField.text = "";
            tmpTextField.text = "";
            int currentIndex = 0;
            while (tmpNameField.text != sentence.name)
            {
                tmpNameField.text += sentence.name[currentIndex];
                currentIndex += 1;
                yield return new WaitForSeconds(speedText);
            }

            currentIndex = 0;
            while (tmpTextField.text != sentence.text && !_isSkip)
            {
                tmpTextField.text += sentence.text[currentIndex];
                currentIndex += 1;
                yield return new WaitForSeconds(speedText);
            }

            if (_isSkip && tmpNameField.text == sentence.name)
            {
                tmpTextField.text = sentence.text;
            }
        
            _isWait = true;
            while (_isWait)
            {
                yield return null;
            }
            _isSkip = false;
        }
    }
}
