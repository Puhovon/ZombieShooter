using System;
using System.Collections;
using Configs.Enemy;
using Enemies.EnemyWave.Factory;
using Global;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Enemies.EnemyWave
{
    public class EnemyWaveController : MonoBehaviour
    {
        [SerializeField] private WaveConfig _config;
        [SerializeField] private Transform[] _spawnPointPositions;
        [SerializeField] private int _waveCount, _enemiesToSpawn, _enemiesCount;

        private Random _random = new Random();
        private EnemyFactory _factory;
        
        public event Action<int> EnemyCountChanged; 

        [Inject]
        private void Construct(EnemyFactory factory)
        {
            _factory = factory;
            _enemiesToSpawn = _config.StartEnemiesCount;
        }

        private void Start()
        {
            SpawnWave();
        }
        // TODO : Create Pool Of Enemies 
        private void SpawnWave()
        {
            _waveCount++;
            for (int i = 0; i < _enemiesToSpawn; i++)
            {
                var point = _random.Next(0, _spawnPointPositions.Length);
                ActivateEnemy(i, _spawnPointPositions[point]);
                _enemiesCount++;
            }
            EnemyCountChanged?.Invoke(_enemiesCount);
        }

        private void ActivateEnemy(int i, Transform spawnPoint)
        {
            var enemy = _factory.SpawnEnemy();
            var health = enemy.GetComponent<Health>();
            health.OnDie += OnEnemyDie;
            enemy.transform.position = spawnPoint.position;
            enemy.gameObject.SetActive(true);
        }

        private void OnEnemyDie()
        {
            _enemiesCount--;
            EnemyCountChanged?.Invoke(_enemiesCount);
            if (_enemiesCount <= 0)
            {
                StartCoroutine(WaitBeforeSpawnNewWave());
            }
        }

        private IEnumerator WaitBeforeSpawnNewWave()
        {
            yield return new WaitForSeconds(5);
            if (_waveCount % 2 == 0)
            {
                _enemiesToSpawn += _config.CountMagnifier;
            }
            SpawnWave();
        }
    }
}