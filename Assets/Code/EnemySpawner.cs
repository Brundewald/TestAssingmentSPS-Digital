using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner: MonoBehaviour
{
    [SerializeField] private EnemyView _enemyPrefab;
    [SerializeField] private DamageMessenger _damageMessenger;
    [SerializeField] private Transform _enemyParent;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _maxEnemyCount;

    private List<EnemyView> _enemiesPool;

    public event Action<EnemyView> EnemySpawned = delegate {  };

    public void CreateEnemyPool(PlayerView playerView)
    {
        _enemiesPool = new List<EnemyView>();
        
        for (var i = 0; i < _maxEnemyCount; i++)
        {
            var enemyView = Instantiate(_enemyPrefab, _enemyParent, true);
            enemyView.gameObject.SetActive(false);
            enemyView.Setup(playerView, _damageMessenger);
            _enemiesPool.Add(enemyView);    
        }
    }

    public void SpawnEnemies(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var positionNumber = Random.Range(0, 1);
            var enemy = _enemiesPool[i];
            enemy.transform.position = _spawnPoints[positionNumber].transform.position;
            enemy.ResetUnit();
            EnemySpawned.Invoke(enemy);
        }
    }
}