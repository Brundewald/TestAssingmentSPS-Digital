using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseStatFieldView : MonoBehaviour
{
    [SerializeField] public Button _enhanceButton;
    [SerializeField] private TextMeshProUGUI _currentStatLevel;
    [SerializeField] private TextMeshProUGUI _currentStatValue;
    [SerializeField] private TextMeshProUGUI _costField;
    private int _enhanceCost;
    public int EnhanceCost => _enhanceCost;
    
    public event Action<string> EnhanceStat = delegate {  };

    protected void UpdateStatLevel(int value)
    {
        _currentStatLevel.text = $"Lv.{value}";
    }

    protected void UpdateStatValue(int value)
    {
        _currentStatValue.text = $"{value}";
    }

    protected void UpdateCostField(int value)
    {
        _costField.text = $"{value} MP";
        _enhanceCost = value;
    }

    public void UpdateButton(bool value)
    {
        _enhanceButton.interactable = value;
    }

    public virtual void OnPropertyChanged(string propertyName, object value)
    {
    }


    protected void UpdateStat(string value)
    {
        EnhanceStat.Invoke(value);
    }

    private void OnDestroy()
    {
        _enhanceButton.onClick.RemoveAllListeners();
    }
}