using System;
using System.Collections;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBoostrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

        private GameCycleHandler _gameCycle;
        private bool _running;

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
            Debug.Log("Initialization of gameplay scene");

            _gameCycle = _container.Resolve<GameCycleHandler>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start of gameplay scene");

            _gameCycle.Run();
            _running = true;
        }

        public void Update()
        {
            if (_running)
                _gameCycle.Update(Time.deltaTime);
        }
    }
}