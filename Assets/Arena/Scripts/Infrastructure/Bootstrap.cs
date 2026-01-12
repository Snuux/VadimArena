using System;
using System.Collections;
using Arena.Scripts.Configs;
using Arena.Scripts.Game;
using Arena.Scripts.Infrastructure.GameCycle;
using Arena.Scripts.Infrastructure.GameCycle.Conditions;
using Arena.Scripts.Infrastructure.Spawners;
using Arena.Scripts.UI;
using UnityEngine;

namespace Arena.Scripts.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private WinConditions _winCondition;
        [SerializeField] private DefeatConditions _defeatCondition;

        [SerializeField] private float _targetTimeUntilDeath;
        [SerializeField] private int _targetDeadEnemiesCount;
        [SerializeField] private int _targetSpawnedEnemiesCount;

        private ControllersUpdateService _controllersUpdateService;
        private GameCycle.GameCycle _gameCycle;
        private EnemiesCountHandler _enemiesCountHandler;
        private SpawnEnemiesInSpawnPointsHandler _spawnEnemiesInSpawnPointsHandler;
        private EnemySpawnerHandler _enemySpawnerHandler;
        private BulletFactory _heroBulletFactory;
        private Hero _hero;

        private bool _gameCycleEnded;

        private void Awake()
        {
            StartCoroutine(StartProcess());
        }

        private IEnumerator StartProcess()
        {
            _controllersUpdateService = new ControllersUpdateService();
            _enemiesCountHandler = new EnemiesCountHandler();

            HeroConfig heroConfig = Resources.Load<HeroConfig>("Configs/HeroConfig");
            EnemyConfig enemyConfig = Resources.Load<EnemyConfig>("Configs/EnemyConfig");

            ControllersFactory controllersFactory = new ControllersFactory();
            AllObjectsFactory allObjectsFactory = new AllObjectsFactory();

            _heroBulletFactory = new BulletFactory();

            HeroFactory heroFactory = new HeroFactory(_controllersUpdateService, controllersFactory, allObjectsFactory);
            EnemyFactory enemyFactory = new EnemyFactory(_controllersUpdateService, controllersFactory,
                allObjectsFactory, _enemiesCountHandler);

            _enemySpawnerHandler = new EnemySpawnerHandler(enemyFactory);

            _spawnEnemiesInSpawnPointsHandler =
                new SpawnEnemiesInSpawnPointsHandler(this, _enemySpawnerHandler);

            _hero = heroFactory.CreateDice(heroConfig, _heroBulletFactory);
            _spawnEnemiesInSpawnPointsHandler.CreateCoroutineForSpawners(enemyConfig);

            _gameCycle = new GameCycle.GameCycle(
                _hero,
                _enemiesCountHandler,
                _targetTimeUntilDeath,
                _targetDeadEnemiesCount,
                _targetSpawnedEnemiesCount,
                _winCondition,
                _defeatCondition
            );

            _gameCycle.Win += OnGameConditionCheckerWin;
            _gameCycle.Defeat += OnGameConditionCheckerDefeat;

            yield return new WaitForSeconds(1.5f);
        }

        private void Update()
        {
            _controllersUpdateService?.Update(Time.deltaTime);
            _gameCycle?.Update(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

        private void OnGUI()
        {
            _gameCycle.OnGUI();
        }

        private void OnGameConditionCheckerDefeat()
        {
            OnGameCycleEnded();
            Debug.Log("Defeat");
        }

        private void OnGameConditionCheckerWin()
        {
            OnGameCycleEnded();
            Debug.Log("Win");
        }

        private void OnGameCycleEnded()
        {
            if (_gameCycleEnded == false)
                _gameCycleEnded = true;
            else
                return;

            _gameCycle.Win -= OnGameConditionCheckerWin;
            _gameCycle.Defeat -= OnGameConditionCheckerDefeat;

            _spawnEnemiesInSpawnPointsHandler.StopCoroutineForSpawners();

            _enemySpawnerHandler.ClearEnemies();
            _heroBulletFactory.ClearBullets();
            
            if (_hero.IsDead == false)
                _hero.Destroy();
        }
    }
}