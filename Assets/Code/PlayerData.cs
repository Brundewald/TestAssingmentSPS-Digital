using System;
using TMPro;

public class PlayerData
{
    private int _hp;
    private int _maxHP;
    private int _attackSpeed;
    private int _damage;
    private int _enhanceCost;
    private int _hpStatLevel;
    private int _attackSpeedStatLevel;
    private int _damageStatLevel;
    private int _damageEnhanceCost;
    private int _attackSpeedEnhanceCost;
    private int _hpEnhanceCost;
    private int _mpCount;
    private int _baseDamage;
    private int _baseAttackSpeed;
    private int _hpModifier;

    public int HP
    {
        get => _hp;
        set
        {
            if (value != _hp)
            {
                _hp = value;
                PropertyChanged.Invoke(nameof(HP), value);
            }
        }
    }

    public int MaxHP
    {
        get => _maxHP;
        set
        {
            if (value != _maxHP)
            {
                _maxHP = value;
                PropertyChanged.Invoke(nameof(MaxHP), value);
            }
        }
    }

    public int AttackSpeed
    {
        get => _attackSpeed;
        set
        {
            if (value != _attackSpeed)
            {
                _attackSpeed = value;
                PropertyChanged.Invoke(nameof(AttackSpeed), value);
            }
        }
    }

    public int Damage
    {
        get => _damage;
        set
        {
            if (value != _damage)
            {
                _damage = value;
                PropertyChanged.Invoke(nameof(Damage), value);
            }
        }
    }

    public int EnhanceCost
    {
        get => _enhanceCost;
        set
        {
            if (value != _enhanceCost)
            {
                _enhanceCost = value;
                PropertyChanged.Invoke(nameof(EnhanceCost), value);
            }
        }
    }

    private int HPStatLevel
    {
        get => _hpStatLevel;
        set
        {
            if (value != _hpStatLevel)
            {
                _hpStatLevel = value;
                PropertyChanged.Invoke(nameof(HPStatLevel), value);
            }
        }
    }

    private int AttackSpeedStatLevel 
    {
        get => _attackSpeedStatLevel;
        set
        {
            if (value != _attackSpeedStatLevel)
            {
                _attackSpeedStatLevel = value;
                PropertyChanged.Invoke(nameof(AttackSpeedStatLevel), value);
            }
        }
    }

    private int DamageStatLevel
    {
        get => _damageStatLevel;
        set
        {
            if (value != _damageStatLevel)
            {
                _damageStatLevel = value;
                PropertyChanged.Invoke(nameof(DamageStatLevel), value);
            }
        }
    }
    
    private int DamageEnhanceCost
    {
        get => _damageEnhanceCost;
        set
        {
            if (value != _damageEnhanceCost)
            {
                _damageEnhanceCost = value;
                PropertyChanged.Invoke(nameof(DamageEnhanceCost), value);
            }
        }
    }
    private int AttackSpeedEnhanceCost
    {
        get => _attackSpeedEnhanceCost;
        set
        {
            if (value != _attackSpeedEnhanceCost)
            {
                _attackSpeedEnhanceCost = value;
                PropertyChanged.Invoke(nameof(AttackSpeedEnhanceCost), value);
            }
        }
    }
    private int HPEnhanceCost
    {
        get => _hpEnhanceCost;
        set
        {
            if (value != _hpEnhanceCost)
            {
                _hpEnhanceCost = value;
                PropertyChanged.Invoke(nameof(HPEnhanceCost), value);
            }
        }
    }

    public int MPCount
    {
        get => _mpCount;
        set
        {
            if (value != _mpCount)
            {
                _mpCount = value;
                PropertyChanged.Invoke(nameof(MPCount), value);
            }
        }
    }

    public event Action<string, object> PropertyChanged = delegate{};


    public void SetupData(GameplaySettings gameplaySettings)
    {
        _baseDamage = gameplaySettings.PlayerDamage;
        _baseAttackSpeed = gameplaySettings.PlayerAttackSpeed;
        _hpModifier = gameplaySettings.HPModifier;
        
        HP = gameplaySettings.PlayerMaxHp;
        MaxHP = gameplaySettings.PlayerMaxHp;
        AttackSpeed = gameplaySettings.PlayerAttackSpeed;
        Damage = gameplaySettings.PlayerDamage;
        EnhanceCost = gameplaySettings.EnhanceCost;
        HPStatLevel = gameplaySettings.HpStatLevel;
        AttackSpeedStatLevel = gameplaySettings.AttackSpeedLevel;
        DamageStatLevel = gameplaySettings.DamageStatLevel;
        MPCount = gameplaySettings.InitialMPCount;
        DamageEnhanceCost = _enhanceCost;
        AttackSpeedEnhanceCost = _enhanceCost;
        HPEnhanceCost = _enhanceCost;
    }

    public void UpdateHPStat()
    {
        MPCount -= HPEnhanceCost;
        HPEnhanceCost += _enhanceCost;
        HPStatLevel++;
        MaxHP += _hpModifier;
        HP = MaxHP;
    }
    public void UpdateDamageStat()
    {
        MPCount -= DamageEnhanceCost;
        DamageEnhanceCost += _enhanceCost;
        DamageStatLevel++;
        Damage += _baseDamage;
    }
    public void UpdateAttackSpeedStat()
    {
        MPCount -= AttackSpeedEnhanceCost;
        AttackSpeedEnhanceCost += _enhanceCost;
        AttackSpeedStatLevel++;
        AttackSpeed += _baseAttackSpeed;
    }
}