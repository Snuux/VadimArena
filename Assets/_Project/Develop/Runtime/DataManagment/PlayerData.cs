using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;

namespace _Project.Develop.Runtime.DataManagment
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;   
    }
}