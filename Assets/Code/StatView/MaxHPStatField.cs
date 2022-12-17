public class MaxHPStatField : BaseStatFieldView
{
    private void Awake()
    {
        _enhanceButton.onClick.AddListener(() => UpdateStat(nameof(MaxHPStatField)));
    }

    private void OnDestroy()
    {
        _enhanceButton.onClick.RemoveAllListeners();
    }
    
    public override void OnPropertyChanged(string propertyName, object value)
    {
        switch (propertyName)
        {
            case "MaxHP":
                UpdateStatValue((int) value);
                break;
            case "HPEnhanceCost":
                UpdateCostField((int) value);
                break;
            case "HPStatLevel":
                UpdateStatLevel((int) value);
                break;
        }
    }
}