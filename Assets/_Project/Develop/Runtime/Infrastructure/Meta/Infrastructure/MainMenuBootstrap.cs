using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Infrastructure.ConfigsManagment;
using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Configs;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBoostrap
    {
        private DIContainer _container;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Initialization of meta scene");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start of meta scene");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                CreateGameplayWithSequence(
                    _container.Resolve<ConfigsProviderService>().GetConfig<NumbersSequence>());

            if (Input.GetKeyDown(KeyCode.Alpha2))
                CreateGameplayWithSequence(
                    _container.Resolve<ConfigsProviderService>().GetConfig<SymbolsSequence>());
        }

        private void CreateGameplayWithSequence(ISequence sequence)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(
                Scenes.Gameplay, new GameplayInputArgs(
                    sequence.Length,
                    sequence.Symbols
                )));
        }
    }
}