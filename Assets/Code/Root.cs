using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Code
{
    public class Root: MonoBehaviour
    {
        [SerializeField] private PlayerView _player;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private UIHandler _uiHandler;
        [SerializeField] private GameplaySettings _settings;
        [SerializeField] private CloudGenerator _cloudGenerator;
        [SerializeField] private int _targetFrameRate = 40;
        [SerializeField] private float _duration = 10f;
        private PlayerData _playerData;
        private Sequence _sequence;

        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
            _playerData = new PlayerData();
            SubscribeToEvents();
            _player.PreparePlayer(_playerData);
            _enemySpawner.CreateEnemyPool(_player);
            _uiHandler.Setup(_playerData);
            _playerData.SetupData(_settings);
            _cloudGenerator.GenerateCloudPull();
            SpawnEnemy();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            _player.EncounterCompleted += OnEncounterCompleted;
            _enemySpawner.EnemySpawned += _player.AddTarget;
        }

        private void UnsubscribeFromEvents()
        {
            _player.EncounterCompleted -= OnEncounterCompleted;
            _enemySpawner.EnemySpawned -= _player.AddTarget;
        }

        private void OnEncounterCompleted()
        { 
            _cloudGenerator.StartAnimation(SpawnEnemy, _duration);
        }

        private void SpawnEnemy()
        {
            _enemySpawner.SpawnEnemies(2);
        }
    }
}