using System.Collections.Generic;
using Arena.Scripts.Configs;
using Arena.Scripts.Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arena.Scripts.Infrastructure.Spawners
{
    public class EnemySpawnerHandler //тут не только спаун, но и список врагов которых заспаунил
    {
        private EnemyFactory _enemyFactory;
        private List<Enemy> _allEnemies;

        public IReadOnlyList<Enemy> AllEnemies => _allEnemies;

        public EnemySpawnerHandler(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;

            _allEnemies = new();
        }

        public List<Enemy> Spawn(
            EnemyConfig enemyConfig,
            Vector3 spawnPosition)
        {
            List<Enemy> spawnedEnemies = new();

            for (int i = 0; i < enemyConfig.SpawnCount; i++)
            {
                Vector3 position = RandomPointOnRadiusXZ(spawnPosition, enemyConfig.SpawnRadius);
                Enemy instance = _enemyFactory.CreateEnemy(enemyConfig, position);
                spawnedEnemies.Add(instance);
            
                instance.Destroyed += _ => {
                    _allEnemies.Remove(instance);
                };
            }
            
            _allEnemies.AddRange(spawnedEnemies);
            
            return spawnedEnemies;
        }

        Vector3 RandomPointOnRadiusXZ(Vector3 center, float radius)
        {
            Vector2 p = Random.insideUnitCircle * radius;
            return new Vector3(center.x + p.x, center.y, center.z + p.y);
        }

        public void ClearEnemies()
        {
            for (var index = _allEnemies.Count - 1; index >= 0; index--)
            {
                var enemy = _allEnemies[index];
                
                enemy.TakeDamage(float.PositiveInfinity);//Destroy();
            }
        }
    }
}