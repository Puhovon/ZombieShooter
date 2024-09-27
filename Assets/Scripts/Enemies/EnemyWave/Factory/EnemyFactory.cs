using System.IO;
using UnityEngine;
using Zenject;

namespace Enemies.EnemyWave.Factory
{
    public class EnemyFactory
    {
        private const string Enemy = "Enemy";
        private const string EnemyPath = "Enemies";
        private GameObject _enemy;
        private IInstantiator _instantiator;
        
        [Inject]
        private void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
            Load();
        }

        public Enemy SpawnEnemy()
        {
            var enemy = _instantiator.InstantiatePrefab(_enemy).GetComponent<Enemy>();
            return enemy;
        }

        private void Load()
        {
            _enemy = Resources.Load<GameObject>(Path.Combine(EnemyPath, Enemy));
        }
    }
}