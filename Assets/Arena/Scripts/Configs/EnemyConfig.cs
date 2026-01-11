using System.Collections.Generic;
using Arena.Scripts.Game;
using UnityEngine;

namespace Arena.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/EnemyConfig", fileName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public Enemy Prefab { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float PushForce { get; private set; }
        [field: SerializeField] public float TimeBetweenPushes { get; private set; }
        
        [field: SerializeField] public List<Vector3> SpawnPoints { get; private set; }
        [field: SerializeField] public float SpawnRadius { get; private set; }
        [field: SerializeField] public float TimeBetweenSpawns { get; private set; }
        [field: SerializeField] public float SpawnCount { get; private set; }
        [field: SerializeField] public float MaxLinearVelocity { get; private set; }
        [field: SerializeField] public float MaxAngularVelocity { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
    }
}