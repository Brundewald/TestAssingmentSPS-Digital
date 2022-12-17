using UnityEngine;

[CreateAssetMenu(menuName = "Settings", fileName = "GameplaySettings")]
public class GameplaySettings: ScriptableObject
{
    [SerializeField] private int _playerMaxHp;
    [SerializeField] private int _playerAttackSpeed;
    [SerializeField] private int _playerDamage;
    [SerializeField] private int _enhanceCost;
    [SerializeField] private int _hpStatLevel;
    [SerializeField] private int _damageStatLevel;
    [SerializeField] private int _attackSpeedLevel;
    [SerializeField] private int _initialMPCount = 5;
    [SerializeField] private int _hpModifier = 30;
    
    public int HPModifier => _hpModifier;
    public int PlayerMaxHp => _playerMaxHp;
    public int PlayerAttackSpeed => _playerAttackSpeed;
    public int PlayerDamage => _playerDamage;
    public int EnhanceCost => _enhanceCost;
    public int HpStatLevel => _hpStatLevel;
    public int DamageStatLevel => _damageStatLevel;
    public int AttackSpeedLevel => _attackSpeedLevel;
    public int InitialMPCount => _initialMPCount;
}