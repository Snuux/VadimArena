using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure.Gameplay
{
    public class GameCycleHandler
    {
        private DIContainer _container;

        private RandomSymbolsSequenceService _randomSymbolsSequenceService;
        private GameFinishStateHandler _gameFinishStateHandler;
        private InputSequenceHandler _inputSequenceHandler;

        public GameCycleHandler(DIContainer container)
        {
            _container = container;
        }

        public void Run()
        {
            _randomSymbolsSequenceService = _container.Resolve<RandomSymbolsSequenceService>();
            _randomSymbolsSequenceService.GenerateSequence();

            _gameFinishStateHandler = _container.Resolve<GameFinishStateHandler>();

            _inputSequenceHandler = _container.Resolve<InputSequenceHandler>();
            _inputSequenceHandler.Clear();

            Debug.Log("Generated Sequence:" + string.Join("", _randomSymbolsSequenceService.RandomSequence));
        }

        public void Update(float deltaTime)
        {
            if (_gameFinishStateHandler.State == GameFinishState.Running)
            {
                _inputSequenceHandler.ProcessInputKeys();

                _gameFinishStateHandler.SetStateBySequenceEquality(
                    _inputSequenceHandler.InputSymbols,
                    _randomSymbolsSequenceService.Length,
                    _randomSymbolsSequenceService.IsSameSequence);

                _gameFinishStateHandler.PrintState();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    SwitchSceneTo(Scenes.MainMenu);
            }
        }

        private void SwitchSceneTo(string sceneName)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(sceneName));
        }
    }
}