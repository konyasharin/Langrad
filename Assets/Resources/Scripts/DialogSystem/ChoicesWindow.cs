using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesWindow : MonoBehaviour
{
    private static ChoicesWindow _instance;
    [SerializeField] private Button choiceButton;
    
    [Range(0f, 1f)]
    [SerializeField] 
    private float _showTime;

    public static ChoicesWindow GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    public IEnumerator Activate(Choice[] choices)
    {
        foreach (var choice in choices)
        {
            yield return ShowChoice(choice);
        }
    }

    private IEnumerator ShowChoice(Choice choice)
    {
        Button newButton = Instantiate(choiceButton, transform);
        Color newButtonColor = newButton.image.color;
        newButton.GetComponentInChildren<TMP_Text>().text = choice.text;
        newButton.onClick.AddListener(delegate { Choose(choice); });
        newButtonColor.a = 0f;
        newButton.image.color = newButtonColor;
        float currentTime = 0;
        while (currentTime < _showTime)
        {
            currentTime += Time.deltaTime;
            newButtonColor.a = Mathf.Clamp01(currentTime / _showTime);
            newButton.image.color = newButtonColor;
            yield return null;
        }
    }

    private void Choose(Choice choice)
    {
        
    }
    
}
