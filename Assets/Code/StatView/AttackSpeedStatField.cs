using System;

public class AttackSpeedStatField : BaseStatFieldView
{
    
    private void Awake()
    {
        _enhanceButton.onClick.AddListener(() => UpdateStat(nameof(AttackSpeedStatField)));
    }

    private void OnDestroy()
    {
        _enhanceButton.onClick.RemoveAllListeners();
    }
    public override void OnPropertyChanged(string propertyName, object value)
    {
        switch (propertyName)
        {
            case "AttackSpeed":
                UpdateStatValue((int) value);
                break;
            case "AttackSpeedEnhanceCost":
                UpdateCostField((int) value);
                break;
            case "AttackSpeedStatLevel":
                UpdateStatLevel((int) value);
                break;
        }
    }
}