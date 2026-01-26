using Assets.Arena.Scripts.Controllers;
using Assets.Arena.Scripts.Game.Components;
using Cinemachine;

namespace Assets.Arena.Scripts.Infrastructure.Spawners
{
    public class ControllersFactory
    {
        public MouseInputShootController CreateMouseShootController(IShootSource shootSource)
        {
            return new MouseInputShootController(shootSource);
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

        public CameraShakeController CreateVirtualCameraShakeController(
            IShootSource shootSource, 
            CinemachineImpulseSource impulseSource, 
            float shakeIntensity)
        {
            return new CameraShakeController(shootSource, impulseSource, shakeIntensity);
        }
    }
}
