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
        private RandomSymbolsSequenceGenerationService _randomSymbolsSequenceGenerationService;
        private SequenceCheckService _sequenceCheckService;
        private GameFinishStateHandler _gameFinishStateHandler;
        private InputSequenceHandler _inputSequenceHandler;
        private ICoroutinesPerformer _coroutinesPerformer;
        private SceneSwitcherService _sceneSwitcherService;

        public GameCycleHandler(
            RandomSymbolsSequenceGenerationService randomSymbolsSequenceGenerationService, 
            GameFinishStateHandler gameFinishStateHandler, 
            InputSequenceHandler inputSequenceHandler, 
            ICoroutinesPerformer coroutinesPerformer, 
            SceneSwitcherService sceneSwitcherService,
            SequenceCheckService sequenceCheckService)
        {
            _randomSymbolsSequenceGenerationService = randomSymbolsSequenceGenerationService;
            _gameFinishStateHandler = gameFinishStateHandler;
            _inputSequenceHandler = inputSequenceHandler;
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _sequenceCheckService = sequenceCheckService;
        }

        public void Run()
        {
            _randomSymbolsSequenceGenerationService.GenerateSequence();
            _inputSequenceHandler.Clear();

            Debug.Log("Generated Sequence:" + _randomSymbolsSequenceGenerationService.Sequence);
            Debug.Log("Repeat it:");
        }

        public void Update(float deltaTime)
        {
            if (_gameFinishStateHandler.State == GameFinishState.Running)
            {
                _inputSequenceHandler.ProcessInputKeys();

                _gameFinishStateHandler.SetStateBySequenceEquality(
                    _inputSequenceHandler.InputSymbols,
                    _randomSymbolsSequenceGenerationService.Sequence,
                    _randomSymbolsSequenceGenerationService.Length,
                    _sequenceCheckService.IsSame);

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