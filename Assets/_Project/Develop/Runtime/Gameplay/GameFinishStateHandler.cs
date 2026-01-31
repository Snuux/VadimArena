using _Project.Develop.Runtime.Infrastructure.DI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure.Gameplay
{
    public class GameFinishStateHandler
    {
        public const string EndMessage = " Press 'Space' to return to main menu";
        
        private GameFinishState _gameFinishState;

        public GameFinishStateHandler()
        {
            _gameFinishState = GameFinishState.Running;
        }

        public GameFinishState State => _gameFinishState;

        public void SetState(GameFinishState state) => _gameFinishState = state;

        public void SetStateBySequenceEquality(
            string inputSymbols,
            string sourceSequence,
            int targetLength,
            Func<string, string, bool> isSameSequence
        )
        {
            if (inputSymbols.Length > targetLength)
                SetState(GameFinishState.Defeat);

            if (inputSymbols.Length < targetLength)
                return;

            if (isSameSequence.Invoke(inputSymbols, sourceSequence))
                SetState(GameFinishState.Win);
            else
                SetState(GameFinishState.Defeat);
        }

        public string GetStateAsString() => _gameFinishState.ToString();
    }
}