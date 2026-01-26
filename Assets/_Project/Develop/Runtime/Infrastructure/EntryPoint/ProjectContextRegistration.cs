using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetManagment;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class ProjectContextRegistration
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);
            container.RegisterAsSingle(CreateConfigsProvider);
            container.RegisterAsSingle(CreateResourcesAssetsLoader);
            container.RegisterAsSingle(CreateSceneLoaderService);
            container.RegisterAsSingle(CreateSceneSwitcherService);
            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);
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
            
            CoroutinesPerformer coroutinesPerformerPrefab = resourcesAssetsLoader
                .Load<CoroutinesPerformer>("Utilities/CoroutinePerformer");

            return Object.Instantiate(coroutinesPerformerPrefab);
        }
        
        private static SceneLoaderService CreateSceneLoaderService(DIContainer c) => 
            new SceneLoaderService();
        
        private static StandardLoadingScreen CreateLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            
            StandardLoadingScreen standardLoadingScreen = resourcesAssetsLoader
                .Load<StandardLoadingScreen>("Utilities/StandardLoadingScreen");

            return Object.Instantiate(standardLoadingScreen);
        }

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new(c.Resolve<SceneLoaderService>(),
                c.Resolve<ILoadingScreen>(),
                c);
    }
}