using _Project.Develop.Runtime.Configs;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutineManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.Meta
{
    class ChangeSceneByLevelTypeService
    {
        DIContainer _container;

        public ChangeSceneByLevelTypeService(DIContainer container)
        {
            _container = container;
        }

        public void ChangeSceneBy(LevelTypes type)
        {
            ConfigsProviderService configsProviderService = _container.Resolve<ConfigsProviderService>();
            LevelsConfig levelsConfig = configsProviderService.GetConfig<LevelsConfig>();
            LevelConfig levelConfig = levelsConfig.GetLevelConfigBy(type);

            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(
                Scenes.Gameplay, new GameplayInputArgs(
                    levelConfig.Length,
                    levelConfig.Symbols
                )));
        }
    }
}