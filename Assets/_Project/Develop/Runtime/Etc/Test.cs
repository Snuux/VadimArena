using System.Collections;
using _Project.Develop.Runtime.Infrastructure.AssetManagment;
using _Project.Develop.Runtime.Infrastructure.ConfigsManagment;
using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Develop.Runtime
{
    public class Test : MonoBehaviour
    {
        private DIContainer _container;

        private void Awake()
        {
            _container = new();
            
            _container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);
            _container.RegisterAsSingle(CreateConfigsProvider);
            _container.RegisterAsSingle(CreateResourcesAssetsLoader);
            
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(LoadConfigs());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ConfigsProviderService configsProvider = _container.Resolve<ConfigsProviderService>();
                TestConfig testConfig = configsProvider.GetConfig<TestConfig>();
                
                Debug.Log(testConfig.Damage);
            }
        }

        private IEnumerator LoadConfigs()
        {
            Debug.Log("Start Load Configs");
            
            ConfigsProviderService configsProvider =  _container.Resolve<ConfigsProviderService>();
            yield return configsProvider.LoadAsync();
            
            Debug.Log("End Load Configs");
        }

        private ConfigsProviderService CreateConfigsProvider(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);
            
            return new ConfigsProviderService(resourcesConfigsLoader);
        }
        
        private ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c) => 
            new ResourcesAssetsLoader();

        private CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            
            var coroutinesPerformerPrefab = resourcesAssetsLoader
                .Load<CoroutinesPerformer>("Utilities/CoroutinePerformer");

            return Instantiate(coroutinesPerformerPrefab);
        }
    }
}