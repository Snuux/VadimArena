using Arena.Scripts.Game;
using Arena.Scripts.Game.Components;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Arena.Scripts.Infrastructure.Spawners
{
    public class AllObjectsFactory
    {
        public Hero CreateHero(
            Hero prefab,
            Bullet bulletPrefab,
            BulletFactory bulletFactory,
            Vector3 spawnPosition,
            float pushForce,
            float health,
            float maxLinearVelocity,
            float maxAngularVelocity,
            float damage,
            float bulletSpeed,
            float bulletLifeTime)
        {
            Hero instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);

            Rigidbody rigidbody = instance.GetComponent<Rigidbody>();

            DiceShooter diceShooter = new DiceShooter(bulletPrefab, instance.transform, bulletSpeed, bulletLifeTime, damage, bulletFactory);
            DiceNumberCalculator diceNumberCalculator = new DiceNumberCalculator(instance.transform);
            DiceShootMover diceShootMover = new DiceShootMover(rigidbody, pushForce);
            DiceHealth diceHealth = new DiceHealth(health);
            CollisionDamageEffect collisionDamageEffect =
                new CollisionDamageEffect(instance, false, damage);

            instance.Initialize(
                diceShooter,
                diceNumberCalculator,
                diceShootMover,
                diceHealth,
                collisionDamageEffect,
                maxLinearVelocity,
                maxAngularVelocity);

            return instance;
        }

        public Enemy CreateEnemy(
            Enemy prefab,
            Vector3 spawnPosition,
            float pushForce,
            float health,
            float maxLinearVelocity,
            float maxAngularVelocity,
            float damage)
        {
            Enemy instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);

            DiceHealth diceHealth = new DiceHealth(health);
            CollisionDamageEffect collisionDamageEffect =
                new CollisionDamageEffect(instance, false, damage);

            instance.Initialize(
                diceHealth,
                collisionDamageEffect,
                pushForce,
                maxLinearVelocity,
                maxAngularVelocity
            );

            return instance;
        }
    }
}