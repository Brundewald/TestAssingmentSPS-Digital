using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour, IHealth, IDamageDealer, IEnemy
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField, Range(0f, 2f)] private float _stoppingDistance;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _hpUpgradeValue;
    [SerializeField] private int _attackSpeed;
    [SerializeField] private int _reward = 10;
    [SerializeField] private int _damage;

    private PlayerView _player;
    private DamageMessenger _damageMessenger;
    private int _hp;
    private bool _cooldown;
    private float _cooldownTime;
    private bool _timeToAttack;


    public event Action<int> Died = delegate {};
    public event Action<int, Vector3> DamageTaken = delegate {};

    public void Setup(PlayerView playerView, DamageMessenger damageMessenger)
    {
        _damageMessenger = damageMessenger;
        DamageTaken += _damageMessenger.ShowMessage;
        _player = playerView;
        _agent.stoppingDistance = _stoppingDistance;
    }

    public void ResetUnit()
    {
        _timeToAttack = false;
        _hp = _maxHP;
        _timeToAttack = false;
        _hp = _maxHP;
        UpdateHealthBar();
        gameObject.SetActive(true);
    }

    private void Update()
    {
        var position = _player.transform.position;
        _agent.SetDestination(position);
        if (Distance() > _stoppingDistance) return;
        if (!_cooldown)
        {
            DealDamage();
            _cooldown = true;
            _cooldownTime = 0;
        }
        else
        {
            var cooldown = (5f / _attackSpeed);
            _cooldownTime += Time.deltaTime;
            _cooldown = _cooldownTime < cooldown;
        }
    }

    private void OnDestroy()
    {
        DamageTaken -= _damageMessenger.ShowMessage;
    }

    private float Distance()
    {
        return Math.Abs((_player.transform.position - transform.position).sqrMagnitude);
    }

    public void DecreaseHP(int value)
    {
        if (_hp > 0)
        {
            _hp -= value;
            UpdateHealthBar();
            DamageTaken.Invoke(value, _healthBar.transform.position);
            if (_hp == 0)
            {
                gameObject.SetActive(false);
                Died.Invoke(_reward);
            }
        }
        
        else
        {
            gameObject.SetActive(false);
            Died.Invoke(_reward);
        }
    }

    public void RestoreHP(int value)
    {
        _hp = _maxHP;
    }

    public void UpdateMaxHP(int value)
    {
        _maxHP += _hpUpgradeValue;
    }

    public void DealDamage()
    {
        if(_player.gameObject.activeInHierarchy) _player.DecreaseHP(_damage);
    }

    private void UpdateHealthBar()
    {
        var value = (float) _hp / _maxHP;
        _healthBar.value = value;
    }
}