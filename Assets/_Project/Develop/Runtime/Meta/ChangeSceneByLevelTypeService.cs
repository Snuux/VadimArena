using System;
using _Project.Develop.Runtime.Infrastructure.ConfigsManagment;
using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Configs;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Meta.Infrastructure
{
    class ChangeSceneByLevelTypeService
    {
        private ConfigsProviderService _configsProviderService;
        private SceneSwitcherService _sceneSwitcherService;
        private ICoroutinesPerformer _coroutinesPerformer;

        public ChangeSceneByLevelTypeService(ConfigsProviderService configsProviderService, SceneSwitcherService sceneSwitcherService, ICoroutinesPerformer coroutinesPerformer)
        {
            _configsProviderService = configsProviderService;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public void Update(Action<float> updateFunc, float deltaTime)
        {
            
        }

        public void ChangeSceneBy(LevelTypes type)
        {
            LevelsConfig levelsConfig = _configsProviderService.GetConfig<LevelsConfig>();
            LevelConfig levelConfig = levelsConfig.GetLevelConfigBy(type);

            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(
                Scenes.Gameplay, new GameplayInputArgs(
                    levelConfig.Length,
                    levelConfig.Symbols
                )));
        }
    }
}