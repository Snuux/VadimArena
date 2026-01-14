using System.Collections;
using _Project.Develop.Runtime.Infrastructure.AssetManagment;
using _Project.Develop.Runtime.Infrastructure.ConfigsManagment;
using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using UnityEngine;

namespace _Project.Develop.Runtime
{
    public class ServiceLocatorTest : MonoBehaviour
    {
        private ServiceLocator _serviceLocator;

        private void Awake()
        {
            _serviceLocator = new ServiceLocator();
            
            _serviceLocator.AddService(CreateResourcesAssetsLoader());
            _serviceLocator.AddService<ICoroutinesPerformer>(CreateCoroutinesPerformer(_serviceLocator));
            _serviceLocator.AddService(CreateConfigsProvider(_serviceLocator));
            
            ICoroutinesPerformer coroutinesPerformer = _serviceLocator.GetService<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(LoadConfigs());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ConfigsProviderService configsProvider = _serviceLocator.GetService<ConfigsProviderService>();
                TestConfig testConfig = configsProvider.GetConfig<TestConfig>();
                
                Debug.Log(testConfig.Damage);
            };
        }

        private IEnumerator LoadConfigs()
        {
            Debug.Log("Start Load Configs");
            
            ConfigsProviderService configsProvider = _serviceLocator.GetService<ConfigsProviderService>();
            yield return configsProvider.LoadAsync();
            
            Debug.Log("End Load Configs");
        }

        private ConfigsProviderService CreateConfigsProvider(ServiceLocator serviceLocator)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = _serviceLocator.GetService<ResourcesAssetsLoader>();
            
            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);
            
            return new ConfigsProviderService(resourcesConfigsLoader);
        }
        
        private ResourcesAssetsLoader CreateResourcesAssetsLoader() => new ResourcesAssetsLoader();

        private CoroutinesPerformer CreateCoroutinesPerformer(ServiceLocator serviceLocator)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = serviceLocator.GetService<ResourcesAssetsLoader>();
            
            var coroutinesPerformerPrefab = resourcesAssetsLoader
                .Load<CoroutinesPerformer>("Utilities/CoroutinePerformer");

            return Instantiate(coroutinesPerformerPrefab);
        }
    }
}