using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;

namespace _Project.Develop.Runtime.DataManagment.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private readonly ConfigsProviderService _configsProvider;
        
        public PlayerDataProvider(
            ISaveLoadService saveLoadService,
            ConfigsProviderService configsProviderService) : base(saveLoadService)
        {
            _configsProvider = configsProviderService;
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData
            {
                WalletData = InitWalletData()
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();
            
            StartWalletConfig walletConfig = _configsProvider.GetConfig<StartWalletConfig>();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                walletData[currencyType] = walletConfig.GetValueFor(currencyType);

            return walletData;
        }
    }
}