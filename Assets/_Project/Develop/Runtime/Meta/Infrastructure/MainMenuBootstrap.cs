using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs;
using _Project.Develop.Runtime.DataManagment;
using _Project.Develop.Runtime.DataManagment.Serializers;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBoostrap
    {
        private DIContainer _container;

        private ReactiveVariable<int> _field;
        private IDisposable _disposable;

        private WalletService _walletService;

        private PlayerData _playerData;
        private IDataSerializer _serializer;
        private string _serializedPlayerData;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Initialization of meta scene");
            _walletService = _container.Resolve<WalletService>();
            _playerData = new PlayerData();
            _playerData.WalletData = new Dictionary<CurrencyTypes, int>
            {
                { CurrencyTypes.Gold, 10 },
                { CurrencyTypes.Diamond, 5 }
            };

            _serializer = new JsonSerializer();


            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start of meta scene");

            _field = new ReactiveVariable<int>(5);

            _disposable = _field.Subscribe(OnFieldChanged);
        }

        private void OnFieldChanged(int arg1, int arg2)
        {
            Debug.Log($"Поле изменилось {arg1} - {arg2}");
            _disposable.Dispose();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _container.Resolve<ChangeSceneByLevelTypeService>().ChangeSceneBy(LevelTypes.Letters);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                _container.Resolve<ChangeSceneByLevelTypeService>().ChangeSceneBy(LevelTypes.Digits);

            if (Input.GetKeyDown(KeyCode.F))
            {
                _field.Value++;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _walletService.Add(CurrencyTypes.Gold, 10);
                Debug.Log("Gold Remain: " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (_walletService.Enough(CurrencyTypes.Gold, 5))
                {
                    _walletService.Spend(CurrencyTypes.Gold, 5);
                    Debug.Log("Gold Remain: " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _serializedPlayerData = _serializer.Serialize(_playerData);
                Debug.Log(_serializedPlayerData);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                _playerData.WalletData = null;
                _playerData = _serializer.Deserialize<PlayerData>(_serializedPlayerData);

                Debug.Log("Gold : " + _playerData.WalletData[CurrencyTypes.Gold]);
                Debug.Log("Diamond : " + _playerData.WalletData[CurrencyTypes.Diamond]);
            }
        }
    }
}