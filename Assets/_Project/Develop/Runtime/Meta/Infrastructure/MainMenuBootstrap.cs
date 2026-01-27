using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Gameplay.Features.Wallet;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBoostrap
    {
        private DIContainer _container;

        private ReactiveVariable<int> _field;
        private IDisposable _disposable;

        private WalletService _walletService;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Initialization of meta scene");
            _walletService = _container.Resolve<WalletService>();

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
        }
    }
}