using _Project.Develop.Runtime.Infrastructure.AssetManagment;
using _Project.Develop.Runtime.Infrastructure.ConfigsManagment;
using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class EntryPointRegistration
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);
            container.RegisterAsSingle(CreateConfigsProvider);
            container.RegisterAsSingle(CreateResourcesAssetsLoader);
        }
        
        private static ConfigsProviderService CreateConfigsProvider(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);
            
            return new ConfigsProviderService(resourcesConfigsLoader);
        }
        
        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c) => 
            new ResourcesAssetsLoader();

        private static CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            
            var coroutinesPerformerPrefab = resourcesAssetsLoader
                .Load<CoroutinesPerformer>("Utilities/CoroutinePerformer");

            return Object.Instantiate(coroutinesPerformerPrefab);
        }
    }
}