using Assets.Arena.Scripts.Game;
using UnityEngine;

namespace Assets.Arena.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/HeroConfig", fileName = "HeroConfig")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField] public Hero Prefab { get; private set; }
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
        [field: SerializeField] public Vector3 SpawnPosition { get; private set; }
        [field: SerializeField] public float PushForce { get; private set; }
        [field: SerializeField] public float ShakeIntensity { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float MaxLinearVelocity { get; private set; }
        [field: SerializeField] public float MaxAngularVelocity { get; private set; }
        [field: SerializeField] public float BulletSpeed { get; private set; }
        [field: SerializeField] public float BulletLifetime { get; private set; }
    }
}