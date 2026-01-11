using Arena.Scripts.Game;
using Arena.Scripts.Game.Components;
using UnityEngine;

namespace Arena.Scripts.Controllers
{
    public class TimedRandomPushableController : Controller
    {
        private readonly IPushable _pushable;
        
        private readonly float _targetTime;
        private float _currentTime;
        private float _pushForce;
        
        public TimedRandomPushableController(IPushable pushable, float targetTime, float pushForce)
        {
            _pushable = pushable;
            _targetTime = targetTime;
            _pushForce = pushForce;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            _currentTime += deltaTime;

            if (_currentTime >= _targetTime)
            {
                Vector3 randomDirection = Random.insideUnitSphere;
                
                _pushable.Push(randomDirection * _pushForce, Vector3.zero);
                _currentTime = 0f;
            }
        }
    }
}