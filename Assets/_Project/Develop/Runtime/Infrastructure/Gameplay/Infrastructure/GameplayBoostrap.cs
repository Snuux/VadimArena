using System;
using System.Collections;
using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Gameplay.Infrastructure
{
    public class GameplayBoostrap : SceneBoostrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;
            
            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"You are on LEVEL: {_inputArgs.LevelNumber}");
            Debug.Log("Initialization of gameplay scene");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start of gameplay scene");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
                coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }
        }
    }
}