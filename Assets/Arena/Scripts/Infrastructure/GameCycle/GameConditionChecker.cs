using System;

namespace Arena.Scripts.Infrastructure.GameCycle
{
    class GameConditionChecker
    {
        public event Action Reached;

        private Func<bool> _condition;

        public GameConditionChecker(Func<bool> condition)
        {
            _condition = condition;
        }

        public void Update(float deltaTime)
        {
            if (_condition.Invoke())
                Reached?.Invoke();
        }
    }
}