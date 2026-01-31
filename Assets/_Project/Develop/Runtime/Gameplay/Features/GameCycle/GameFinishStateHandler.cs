using System;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.GameCycle
{
    public class GameFinishStateHandler
    {
        private GameFinishState _gameFinishState;

        public GameFinishStateHandler()
        {
            _gameFinishState = GameFinishState.Running;
        }

        public GameFinishState State => _gameFinishState;

        public void SetState(GameFinishState state) => _gameFinishState = state;

        public void SetStateBySequenceEquality(string inputSymbols, int targetLength, Predicate<string> isSameSequence)
        {
            if (inputSymbols.Length > targetLength)
                SetState(GameFinishState.Defeat);

            if (inputSymbols.Length < targetLength)
                return;

            if (isSameSequence.Invoke(inputSymbols))
                SetState(GameFinishState.Win);
            else
                SetState(GameFinishState.Defeat);
        }

        public void PrintState()
        {
            switch (State)
            {
                case GameFinishState.Win:
                    Debug.Log("Win! Press 'Space' to return to main menu");
                    break;
                case GameFinishState.Defeat:
                    Debug.Log("Defeat! Press 'Space' to return to main menu");
                    break;
            }
        }
    }
}