using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Infrastructure.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.Infrastructure.Meta
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