using System.Collections;
using System.Collections.Generic;
using Arena.Scripts.Configs;
using UnityEngine;

namespace Arena.Scripts.Infrastructure.Spawners
{
    public class SpawnEnemiesInSpawnPointsHandler
    {
        private EnemySpawnerHandler _enemySpawnerHandler;
        private MonoBehaviour _coroutineStarter;

        private List<Coroutine> _spawnCoroutine;

        public SpawnEnemiesInSpawnPointsHandler(
            MonoBehaviour coroutineStarter,
            EnemySpawnerHandler enemySpawnerHandler)
        {
            _coroutineStarter = coroutineStarter;
            _enemySpawnerHandler = enemySpawnerHandler;

            _spawnCoroutine = new();
        }

        private IEnumerator Spawn(EnemyConfig enemyConfig, Vector3 spawnPosition)
        {
            while (true)
            {
                yield return new WaitForSeconds(1.5f);

                _enemySpawnerHandler.Spawn(
                    enemyConfig,
                    spawnPosition);
            }
        }

        public void CreateCoroutineForSpawners(EnemyConfig enemyConfig)
        {
            List<Vector3> spawnPoints = enemyConfig.SpawnPoints;

            foreach (Vector3 spawnPoint in spawnPoints)
                _spawnCoroutine.Add(
                    _coroutineStarter.StartCoroutine(
                        Spawn(enemyConfig, spawnPoint))
                );
        }

        public void StopCoroutineForSpawners()
        {
            foreach (Coroutine coroutine in _spawnCoroutine)
                _coroutineStarter.StopCoroutine(coroutine);
        }
    }
}