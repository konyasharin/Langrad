using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChoicesWindow : MonoBehaviour
{
    public static ChoicesWindow Instance { get; private set; }
    [SerializeField] private Button choiceButton;
    
    [Range(0f, 1f)]
    [SerializeField] 
    private float showTime;

    private bool _isActive = false;
    private bool _isWait = false;
    private readonly List<Button> _choiceButtons = new List<Button>();

    public bool GetIsActive()
    {
        return _isActive;
    }

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Activate(Choice[] choices)
    {
        _isActive = true;
        foreach (var choice in choices)
        {
            yield return StartCoroutine(ShowChoice(choice));
        }
        _isWait = true;
    }
    
    public IEnumerator Deactivate()
    {
        for (int i = _choiceButtons.Count - 1; i >= 0; i--)
        {
            yield return HideChoice(_choiceButtons[i]);
        }

        _isActive = false;
    }

    private IEnumerator ShowChoice(Choice choice)
    {
        Button newButton = Instantiate(choiceButton, transform);
        _choiceButtons.Add(newButton);
        Color newButtonColor = newButton.image.color;
        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = choice.text;
        newButton.onClick.AddListener(delegate { Choose(choice); });
        newButtonColor.a = 0f;
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, newButtonColor.a);
        newButton.image.color = newButtonColor;
        float currentTime = 0;
        while (currentTime < showTime)
        {
            currentTime += Time.deltaTime;
            newButtonColor.a = Mathf.Clamp01(currentTime / showTime);
            newButton.image.color = newButtonColor;
            buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, newButtonColor.a);
            yield return null;
        }
    }

    private IEnumerator HideChoice(Button button)
    {
        Color newButtonColor = button.image.color;
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        float currentTime = 0;
        while (currentTime < showTime)
        {
            currentTime += Time.deltaTime;
            newButtonColor.a = Mathf.Clamp01((showTime - currentTime) / showTime);
            button.image.color = newButtonColor;
            buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, newButtonColor.a);
            yield return null;
        }
        Destroy(button);
    }

    private void Choose(Choice choice)
    {
        if (_isWait)
        {
            _isWait = false;
            foreach (var plotInfluence in choice.plotInfluences)
            {
               DialogsManager.Instance.ChangePlotInfluence(plotInfluence.plotInfluenceType, plotInfluence.countPlotInfluence);
            }
            StartCoroutine(Deactivate());   
        }
    }
}
