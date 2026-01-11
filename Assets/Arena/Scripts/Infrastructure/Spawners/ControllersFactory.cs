using Arena.Scripts.Controllers;
using Arena.Scripts.Game.Components;
using Cinemachine;

namespace Arena.Scripts.Infrastructure.Spawners
{
    public class ControllersFactory
    {
        public MouseShootController CreateMouseShootController(IShootSource shootSource)
        {
            return new MouseShootController(shootSource);
        }

        public ShootPushableController CreateShootPushableController(
            IPushable pushable, 
            IShootSource shootSource)
        {
            return new ShootPushableController(pushable, shootSource);
        }

        public TimedRandomPushableController CreateTimedRandomPushableController(
            IPushable pushable, 
            float targetTime,
            float pushForce)
        {
            return new TimedRandomPushableController(pushable, targetTime, pushForce);
        }

        public VirtualCameraShakeController CreateVirtualCameraShakeController(
            IShootSource shootSource, 
            CinemachineImpulseSource impulseSource, 
            float shakeIntensity)
        {
            return new VirtualCameraShakeController(shootSource, impulseSource, shakeIntensity);
        }
    }
}
