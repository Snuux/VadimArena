using _Project.Develop.Runtime.Infrastructure.ConfigsManagment;
using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Infrastructure.Meta.Infrastructure;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Process of services registration in menu scene");

            container.RegisterAsSingle(CreateChangeSceneByLevelTypeService);
        }

        private static ChangeSceneByLevelTypeService CreateChangeSceneByLevelTypeService(DIContainer c)
        {
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();

            return new ChangeSceneByLevelTypeService(configsProviderService, sceneSwitcherService, coroutinesPerformer);
        }
    }
}