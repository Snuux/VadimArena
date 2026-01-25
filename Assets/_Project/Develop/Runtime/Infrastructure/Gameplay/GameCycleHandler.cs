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
        private RandomSymbolsSequenceService _randomSymbolsSequenceService;
        private GameFinishStateHandler _gameFinishStateHandler;
        private InputSequenceHandler _inputSequenceHandler;
        private ICoroutinesPerformer _coroutinesPerformer;
        private SceneSwitcherService _sceneSwitcherService;

        public GameCycleHandler(
            RandomSymbolsSequenceService randomSymbolsSequenceService, 
            GameFinishStateHandler gameFinishStateHandler, 
            InputSequenceHandler inputSequenceHandler, 
            ICoroutinesPerformer coroutinesPerformer, 
            SceneSwitcherService sceneSwitcherService)
        {
            _randomSymbolsSequenceService = randomSymbolsSequenceService;
            _gameFinishStateHandler = gameFinishStateHandler;
            _inputSequenceHandler = inputSequenceHandler;
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
        }

        public void Run()
        {
            _randomSymbolsSequenceService.GenerateSequence();
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
            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(sceneName));
        }
    }
}