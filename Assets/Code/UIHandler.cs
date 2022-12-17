using System;
using TMPro;
using UnityEngine;

public class UIHandler: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currencyField;
    [SerializeField] private BaseStatFieldView[] _statsFields;
    private PlayerData _data;

    public void Setup(PlayerData data)
    {
        _data = data;
        _data.PropertyChanged += UpdateMPCounter;
        _currencyField.text = $"MP: 0";
        foreach (var stat in _statsFields)
        {
            _data.PropertyChanged += stat.OnPropertyChanged;
            stat.EnhanceStat += UpdateData;
        }
    }

    private void FixedUpdate()
    {
        foreach (var stat in _statsFields)
        {
            stat.UpdateButton(stat.EnhanceCost <= _data.MPCount);
        }
    }

    private void OnDestroy()
    {
        foreach (var stat in _statsFields)
        {
            _data.PropertyChanged -= stat.OnPropertyChanged;
            stat.EnhanceStat -= UpdateData;
        }

        _data.PropertyChanged -= UpdateMPCounter;
    }

    private void UpdateData(string callerName)
    {
        if (callerName.Equals(nameof(AttackSpeedStatField)))
        {
            _data.UpdateAttackSpeedStat();
        }

        if (callerName.Equals(nameof(DamageStatField)))
        {
            _data.UpdateDamageStat();
        }

        if (callerName.Equals(nameof(MaxHPStatField)))
        {
            _data.UpdateHPStat();
        }
        
    }

    private void UpdateMPCounter(string propertyName, object value)
    {
        if (propertyName.Equals("MPCount"))
        {
            _currencyField.text = $"MP: {value}";
        }
    }
}