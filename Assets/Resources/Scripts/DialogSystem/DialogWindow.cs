using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class DialogWindow : MonoBehaviour
{
    private static DialogWindow _instance;
    [SerializeField] private TMP_Text tmpTextField;
    [SerializeField] private TMP_Text tmpNameField;
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
    [Range(0.1f, 0.5f)]
    [SerializeField]
    private float totalMovementTime;
    [Range(0.05f, 0.1f)]
    [SerializeField]
    private float speedText;

    public static DialogWindow GetInstance()
    {
        return _instance;
    }
    
    private void Awake()
    {
        _instance = this;
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
        yield return StartCoroutine(Move(_rectTransform.anchoredPosition, new Vector2(_rectTransform.anchoredPosition.x, 200)));
        _isActive = true;
    }

    public IEnumerator Deactivate()
    {
        _isActive = false;
        yield return StartCoroutine(Move(_rectTransform.anchoredPosition, new Vector2(_rectTransform.anchoredPosition.x, -200)));
    }

    private IEnumerator Move(Vector2 fromPosition, Vector2 toPosition)
    {
        float currentMovementTime = 0f;
        while (currentMovementTime < totalMovementTime)
        {
            currentMovementTime += Time.deltaTime;
            _rectTransform.anchoredPosition =
                Vector2.Lerp(fromPosition, toPosition, currentMovementTime / totalMovementTime);
            yield return null;
        }
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

    public IEnumerator ShowChoice()
    {
        
    }
}
