using System.Collections;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutineManagment;
using _Project.Develop.Runtime.Utilities.LoadingScreen;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Start of the project, setup app settings");
            SetupAppSettings();

            Debug.Log("Process of registration of all services");
            DIContainer projectContainer = new DIContainer();

            ProjectContextRegistration.Process(projectContainer);

            projectContainer.Resolve<ICoroutinesPerformer>().StartPerform(Initialize(projectContainer));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private IEnumerator Initialize(DIContainer container)
        {
            ILoadingScreen loadingScreen = container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcherService = container.Resolve<SceneSwitcherService>();
            
            Debug.Log("Open loading screen");

            loadingScreen.Show();
            
            Debug.Log("Process of initialization of all services");

            yield return container.Resolve<ConfigsProviderService>().LoadAsync();

            yield return new WaitForSeconds(1f);
            
            Debug.Log("End of initialization of all services");
            
            Debug.Log("Close loading screen");
            loadingScreen.Hide();
            
            Debug.Log("Started new scene...");

            yield return sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}