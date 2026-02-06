using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.DataManagment;
using _Project.Develop.Runtime.DataManagment.DataProviders;
using _Project.Develop.Runtime.DataManagment.DataRepository;
using _Project.Develop.Runtime.DataManagment.KeyStorage;
using _Project.Develop.Runtime.DataManagment.Serializers;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.AssetManagment;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutineManagment;
using _Project.Develop.Runtime.Utilities.LoadingScreen;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;
using Object = UnityEngine.Object;



namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
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
            container.RegisterAsSingle(CreateWalletService).NonLazy();
            container.RegisterAsSingle<ISaveLoadService>(CreateSaveLoadService);
            container.RegisterAsSingle(CreatePlayerDataProvider);
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

        private static WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach(CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                currencies[currencyType] = new ReactiveVariable<int>();

            return new WalletService(currencies, c.Resolve<PlayerDataProvider>());
        }

        private static SaveLoadService CreateSaveLoadService(DIContainer c)
        {
            IDataSerializer dataSerializer = new JsonSerializer();
            IDataKeysStorage keysStorage = new MapDataKeysStorage();
            
            string saveFolderPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;

            IDataRepository dataRepository = new LocalFileDataRepository(saveFolderPath, "json");
            
            return new SaveLoadService(dataSerializer, keysStorage, dataRepository);
        }

        private static PlayerDataProvider CreatePlayerDataProvider(DIContainer c) 
            => new(c.Resolve<ISaveLoadService>(), c.Resolve<ConfigsProviderService>());
    }
}