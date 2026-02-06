using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Meta/Wallet/StartWalletConfig",  fileName = "StartWalletConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;
        
        public int GetValueFor(CurrencyTypes currencyType) 
            => _values.First(config => config.Type == currencyType).Value;
        
        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}