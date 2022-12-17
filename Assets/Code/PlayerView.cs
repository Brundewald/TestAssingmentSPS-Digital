using System;
using System.Collections.Generic;
using Code;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour, IHealth, IDamageDealer
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private AmmoView _ammoPrefab;
    [SerializeField] private float _attackDistance = 6f;
    [SerializeField] private DamageMessenger _damageMessenger;
    
    private PlayerData _playerData;
    private Stack<EnemyView> _targetStack;
    private EnemyView _currentTarget;
    private bool _timeToAttack;
    private bool _cooldown;
    private float _cooldownTime;
    
    public event Action<int, Vector3> DamageTaken = delegate {  };
    public event Action EncounterCompleted = delegate {  };

    public void PreparePlayer(PlayerData data)
    {
        _targetStack = new Stack<EnemyView>();
        _playerData = data;
        DamageTaken += _damageMessenger.ShowMessage;
        _playerData.PropertyChanged += UpdateHP;
    }

    private void UpdateHP(string propertyName, object value)
    {
        if (propertyName.Equals("HP"))
        {
            UpdateHealthBar();
        }
    }

    public void AddTarget(EnemyView enemyView)
    {
        _targetStack.Push(enemyView);
        if(_currentTarget is null) ChangeTarget();
    }

    private void Update()
    {
        if(_currentTarget is null) return;
         
        var distance = Distance();
        
        if(distance > _attackDistance) return;
        
        if (!_cooldown)
        {
            _cooldown = true;
            _cooldownTime = 0;
            Fire();
        }
        else
        {
            var cooldown = (2.5f / _playerData.AttackSpeed);
            _cooldownTime += Time.deltaTime;
            _cooldown = _cooldownTime < cooldown;
        }
    }

    private void OnDestroy()
    {
        DamageTaken -= _damageMessenger.ShowMessage;
    }

    public void DecreaseHP(int value)
    {
        var hp = _playerData.HP - value;
        DamageTaken.Invoke(value, _healthBar.transform.position);

        if (hp > 0)
        {
            _playerData.HP = hp;
            UpdateHealthBar();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void RestoreHP(int value)
    {
        var hp = _playerData.HP + value;
        _playerData.HP = hp;
    }

    public void UpdateMaxHP(int value)
    {
        var maxHP = _playerData.MaxHP + value;
        _playerData.MaxHP = maxHP;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        var value = (float) _playerData.HP / _playerData.MaxHP;
        _healthBar.value = value;
    }

    private void Fire()
    {
        if (!_currentTarget.gameObject.activeInHierarchy) return;
        var round = Instantiate(_ammoPrefab);
        round.transform.position = transform.position;
        round.Setup(_currentTarget, DealDamage);
    }

    public void DealDamage()
    {
        _currentTarget.DecreaseHP(_playerData.Damage);
    }

    private float Distance()
    {
        return Math.Abs((_currentTarget.transform.position - transform.position).sqrMagnitude);
    }

    private void ChangeTarget(int reward = 0)
    {
        if (_currentTarget != null)
        {
            _currentTarget.Died -= ChangeTarget;
            _playerData.MPCount += reward;
        }
        if(_targetStack.Count > 0)
        {
            
            _currentTarget = _targetStack.Pop();
            _currentTarget.Died += ChangeTarget;
        }
        else
        {
            _currentTarget = null;
            EncounterCompleted.Invoke();
        }
    }
}