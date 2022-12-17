public class DamageStatField : BaseStatFieldView
{
    private void Awake()
    {
        _enhanceButton.onClick.AddListener(() => UpdateStat(nameof(DamageStatField)));
    }

    private void OnDestroy()
    {
        _enhanceButton.onClick.RemoveAllListeners();
    }
    
    public override void OnPropertyChanged(string propertyName, object value)
    {
        switch (propertyName)
        {
            case "Damage":
                UpdateStatValue((int) value);
                break;
            case "DamageEnhanceCost":
                UpdateCostField((int) value);
                break;
            case "DamageStatLevel":
                UpdateStatLevel((int) value);
                break;
        }
    }
}