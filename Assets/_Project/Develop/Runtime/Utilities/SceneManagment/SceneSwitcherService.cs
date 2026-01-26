using System;
using System.Collections;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagment
{
    public class SceneSwitcherService
    {
        private readonly SceneLoaderService _sceneLoaderService;
        private readonly ILoadingScreen _loadingScreen;
        private readonly DIContainer _projectContainer; // его нужно передавать в сцену

        public SceneSwitcherService(
            SceneLoaderService sceneLoaderService,
            ILoadingScreen loadingScreen,
            DIContainer projectContainer)
        {
            _sceneLoaderService = sceneLoaderService;
            _loadingScreen = loadingScreen;
            _projectContainer = projectContainer;
        }

        public IEnumerator ProcessSwitchTo(string sceneName, IInputSceneArgs sceneArgs = null)
        {
            _loadingScreen.Show();

            yield return _sceneLoaderService.LoadAsync(Scenes.Empty);
            yield return _sceneLoaderService.LoadAsync(sceneName);

            SceneBoostrap sceneBoostrap = Object.FindObjectOfType<SceneBoostrap>();

            if (sceneBoostrap == null)
                throw new NullReferenceException(nameof(sceneBoostrap) + "not found");

            DIContainer sceneContainer = new DIContainer(_projectContainer);
            
            sceneBoostrap.ProcessRegistration(sceneContainer, sceneArgs);
            
            yield return sceneBoostrap.Initialize();

            _loadingScreen.Hide();
            
            sceneBoostrap.Run();
        }
    }
}