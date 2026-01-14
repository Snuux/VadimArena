using System.Collections;
using _Project.Develop.Runtime.Infrastructure.AssetManagment;
using _Project.Develop.Runtime.Infrastructure.ConfigsManagment;
using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using UnityEngine;

namespace _Project.Develop.Runtime
{
    public class Test : MonoBehaviour
    {
        private ICoroutinesPerformer _coroutinesPerformer;
        private ResourcesAssetsLoader _resourcesAssetsLoader;
        private ConfigsProviderService _configsProvider;

        private void Awake()
        {
            _resourcesAssetsLoader = CreateResourcesAssetsLoader();
            _coroutinesPerformer = CreateCoroutinesPerformer();
            _configsProvider = CreateConfigsProvider();
            
            _coroutinesPerformer.StartPerform(LoadConfigs());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                TestConfig testConfig = _configsProvider.GetConfig<TestConfig>();
                Debug.Log(testConfig.Damage);
            };
        }

        private IEnumerator LoadConfigs()
        {
            Debug.Log("Start Load Configs");
            yield return _configsProvider.LoadAsync();
            Debug.Log("End Load Configs");
        }

        private ConfigsProviderService CreateConfigsProvider()
        {
            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(_resourcesAssetsLoader);
            return new ConfigsProviderService(resourcesConfigsLoader);
        }
        
        private ResourcesAssetsLoader CreateResourcesAssetsLoader() => new ResourcesAssetsLoader();

        private CoroutinesPerformer CreateCoroutinesPerformer()
        {
            var coroutinesPerformerPrefab = _resourcesAssetsLoader
                .Load<CoroutinesPerformer>("Utilities/CoroutinePerformer");

            return Instantiate(coroutinesPerformerPrefab);
        }
    }
}