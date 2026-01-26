using Assets.Arena.Scripts.Game.Components;
using Cinemachine;
using UnityEngine;

namespace Assets.Arena.Scripts.Controllers
{
    public class CameraShakeController : Controller
    {
        private readonly IShootSource _shootSource;  
        private readonly CinemachineImpulseSource _impulseSource;

        private readonly float _shakeIntensity;

        public CameraShakeController(IShootSource shootSource, CinemachineImpulseSource impulseSource, float shakeIntensity)
        {
            _shootSource = shootSource;
            _impulseSource = impulseSource;
            _shakeIntensity = shakeIntensity;
        }
        
        private void CameraConstantShake(Vector3 direction)
        {
            //float intensity = ShakeIntensity * _dice.TopDiceValue / (float) MaxDiceSides;
            _impulseSource.GenerateImpulseWithVelocity(Vector3.up * _shakeIntensity);
        }
        
        public override void Enable()
        {
            base.Enable();
        
            _shootSource.Shooted += CameraConstantShake;
        }

        public override void Disable()
        {
            base.Disable();
        
            _shootSource.Shooted -= CameraConstantShake;
        }
    }
}