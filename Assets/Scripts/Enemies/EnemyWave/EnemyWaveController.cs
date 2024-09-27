using System.Collections;
using Configs.Enemy;
using Enemies.EnemyWave.Factory;
using Global;
using UnityEngine;
using Zenject;

namespace Enemies.EnemyWave
{
    public class EnemyWaveController : MonoBehaviour
    {
        [SerializeField] private WaveConfig _config;
        [SerializeField] private Transform[] _spawnPointPositions;
        private EnemyFactory _factory;
        

        [SerializeField] private int _waveCount, _enemiesToSpawn, _enemiesCount;
        
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
            for (int i = 0; i < _enemiesToSpawn; i++)
            {
                ActivateEnemy(i, _spawnPointPositions[0]);
                _enemiesCount++;
            }
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
            if (_enemiesCount <= 0)
            {
                StartCoroutine(WaitBeforeSpawnNewWave());
            }
        }

        private IEnumerator WaitBeforeSpawnNewWave()
        {
            yield return new WaitForSeconds(5);
            SpawnWave();
        }
    }
}