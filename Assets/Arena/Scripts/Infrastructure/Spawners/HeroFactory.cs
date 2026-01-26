using Assets.Arena.Scripts.Configs;
using Assets.Arena.Scripts.Controllers;
using Assets.Arena.Scripts.Game;
using Assets.Arena.Scripts.Helpers;
using Cinemachine;
using UnityEngine;

namespace Assets.Arena.Scripts.Infrastructure.Spawners
{
    public class HeroFactory 
    {
        private ControllersUpdateService _controllersUpdateService;
        private ControllersFactory _controllersFactory;
        private AllObjectsFactory _allObjectsFactory;

        public HeroFactory(
            ControllersUpdateService controllersUpdateService,
            ControllersFactory controllersFactory,
            AllObjectsFactory allObjectsFactory)
        {
            _controllersUpdateService = controllersUpdateService;
            _controllersFactory = controllersFactory;
            _allObjectsFactory = allObjectsFactory;
        }

        public Hero CreateDice(
            HeroConfig config,
            BulletFactory bulletFactory)
        {
            Hero instance = _allObjectsFactory.CreateHero(
                config.Prefab,
                config.BulletPrefab,
                bulletFactory,
                config.SpawnPosition,
                config.PushForce,
                config.Health,
                config.MaxLinearVelocity,
                config.MaxAngularVelocity,
                config.Damage,
                config.BulletSpeed,
                config.BulletLifetime
            );

            CinemachineVirtualCamera followCameraPrefab = Resources.Load<CinemachineVirtualCamera>("DiceFollowCamera");
            CinemachineVirtualCamera followCamera = Object.Instantiate(followCameraPrefab);
        
            CinemachineImpulseSource impulseSourcePrefab = Resources.Load<CinemachineImpulseSource>("DiceImpulseSource");
            CinemachineImpulseSource impulseSource = Object.Instantiate(impulseSourcePrefab);
            
            AttachDestroyWithTarget(instance.gameObject, followCamera.gameObject);
            AttachDestroyWithTarget(instance.gameObject, impulseSource.gameObject);

            followCamera.Follow = instance.transform;

            Controller mouseShootController = _controllersFactory.CreateMouseShootController(instance);
            Controller shootPushableController = _controllersFactory.CreateShootPushableController(instance, instance);
            Controller virtualCameraShakeController = _controllersFactory.CreateVirtualCameraShakeController(instance, impulseSource, config.ShakeIntensity);

            mouseShootController.Enable();
            shootPushableController.Enable();
            virtualCameraShakeController.Enable();

            _controllersUpdateService.Add(mouseShootController, () => instance.IsDead || instance.IsDestroyed);
            _controllersUpdateService.Add(shootPushableController, () => instance.IsDead || instance.IsDestroyed);
            _controllersUpdateService.Add(virtualCameraShakeController, () => instance.IsDead || instance.IsDestroyed);

            return instance;
        }
        
        private void AttachDestroyWithTarget(GameObject parent, GameObject target)
        {
            if (parent == false) 
                return;

            DestroyWithTarget destroyWithTarget = target.AddComponent<DestroyWithTarget>();
            destroyWithTarget.Attach(parent);
        }
    }
}
