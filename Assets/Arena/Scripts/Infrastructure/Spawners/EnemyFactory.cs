using Arena.Scripts.Configs;
using Arena.Scripts.Controllers;
using Arena.Scripts.Game;
using Arena.Scripts.Infrastructure.GameCycle;
using UnityEngine;

namespace Arena.Scripts.Infrastructure.Spawners
{
    public class EnemyFactory
    {
        private ControllersUpdateService _controllersUpdateService;
        private ControllersFactory _controllersFactory;
        private AllObjectsFactory _allObjectsFactory;
        private EnemiesCountHandler _enemiesCountHandler;

        public EnemyFactory(
            ControllersUpdateService controllersUpdateService,
            ControllersFactory controllersFactory,
            AllObjectsFactory allObjectsFactory,
            EnemiesCountHandler enemiesCountHandler)
        {
            _controllersUpdateService = controllersUpdateService;
            _controllersFactory = controllersFactory;
            _allObjectsFactory = allObjectsFactory;
            _enemiesCountHandler = enemiesCountHandler;
        }

        public Enemy CreateEnemy(
            EnemyConfig config,
            Vector3 spawnPosition)
        {
            Enemy instance = _allObjectsFactory.CreateEnemy(
                config.Prefab,
                spawnPosition,
                config.PushForce,
                config.Health,
                config.MaxLinearVelocity,
                config.MaxAngularVelocity,
                config.Damage
                );

            TimedRandomPushableController timedRandomPushableController =
                _controllersFactory.CreateTimedRandomPushableController(instance, config.TimeBetweenPushes, config.PushForce);

            timedRandomPushableController.Enable();

            _controllersUpdateService.Add(timedRandomPushableController, () => instance.IsDead);

            _enemiesCountHandler.Add(instance);//()=> instance.IsDead);
            
            return instance;
        }
    }
}