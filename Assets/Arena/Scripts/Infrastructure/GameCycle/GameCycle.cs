using System;
using Arena.Scripts.Game;
using Arena.Scripts.Infrastructure.GameCycle.Conditions;
using Arena.Scripts.Infrastructure.Spawners;
using UnityEngine;

namespace Arena.Scripts.Infrastructure.GameCycle
{
    public class GameCycle
    {
        public event Action Win
        {
            add => _winConditionChecker.Reached += value;
            remove => _winConditionChecker.Reached -= value;
        }

        public event Action Defeat
        {
            add => _defeatConditionChecker.Reached += value;
            remove => _defeatConditionChecker.Reached -= value;
        }

        private GameConditionChecker _winConditionChecker;
        private GameConditionChecker _defeatConditionChecker;

        private readonly Hero _hero;
        private readonly float _targetTimeUntilDeath;
        private readonly int _targetDeadEnemiesCount;
        private readonly int _targetSpawnedEnemiesCount;
        private readonly EnemiesCountHandler _enemiesCountHandler;
        private readonly GameCycleUI _gameCycleUI;

        private float _currentAliveTime;


        public GameCycle(Hero hero, EnemiesCountHandler enemiesCountHandler, float targetTimeUntilDeath,
            int targetDeadEnemiesCount, int targetSpawnedEnemiesCount, WinConditions winCondition,
            DefeatConditions defeatCondition)
        {
            _hero = hero;
            _targetTimeUntilDeath = targetTimeUntilDeath;
            _targetDeadEnemiesCount = targetDeadEnemiesCount;
            _targetSpawnedEnemiesCount = targetSpawnedEnemiesCount;

            _enemiesCountHandler = enemiesCountHandler;

            ChooseConditions(winCondition, defeatCondition);
            
            _gameCycleUI = new GameCycleUI();
        }

        public void ChooseConditions(WinConditions winCondition, DefeatConditions defeatCondition)
        {
            if (winCondition == WinConditions.TimeUntilDeath)
                _winConditionChecker = new GameConditionChecker(WinConditionCheckerTimeUntilDeath);
            else if (winCondition == WinConditions.DiedEnemiesCount)
                _winConditionChecker = new GameConditionChecker(WinConditionDiedEnemiesCount);
            else
                throw new Exception("Not implemented game condition checker");

            if (defeatCondition == DefeatConditions.DiedHero)
                _defeatConditionChecker = new GameConditionChecker(DefeatConditionDiedHero);
            else if (defeatCondition == DefeatConditions.SpawnedEnemiesCount)
                _defeatConditionChecker = new GameConditionChecker(DefeatConditionSpawnedEnemiesCount);
            else
                throw new Exception("Not implemented game condition checker");
        }

        private bool WinConditionCheckerTimeUntilDeath()
        {
            if (_currentAliveTime >= _targetTimeUntilDeath)
                return true;

            return false;
        }

        private bool WinConditionDiedEnemiesCount()
        {
            if (_enemiesCountHandler.DeadCount >= _targetDeadEnemiesCount)
                return true;

            return false;
        }

        private bool DefeatConditionDiedHero()
        {
            if (_hero.IsDead)
                return true;

            return false;
        }

        private bool DefeatConditionSpawnedEnemiesCount()
        {
            if (_enemiesCountHandler.AliveCount >= _targetSpawnedEnemiesCount)
                return true;

            return false;
        }

        public void Update(float deltaTime)
        {
            _currentAliveTime += deltaTime;

            _winConditionChecker?.Update(deltaTime);
            _defeatConditionChecker?.Update(deltaTime);
        }

        public void OnGUI()
        {
            _gameCycleUI.OnGUI(_currentAliveTime, _enemiesCountHandler.AliveCount,
                _enemiesCountHandler.DeadCount, _targetDeadEnemiesCount, _hero.Health);
        }
    }
}