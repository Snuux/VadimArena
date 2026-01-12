using System.Collections.Generic;
using Arena.Scripts.Configs;
using Arena.Scripts.Controllers;
using Arena.Scripts.Game;
using Arena.Scripts.Game.Components;
using Arena.Scripts.Helpers;
using Cinemachine;
using UnityEngine;

namespace Arena.Scripts.Infrastructure.Spawners
{
    public class BulletFactory
    {
        private List<Bullet> _allBullets = new();

        public IReadOnlyList<Bullet> AllBullets => _allBullets;

        public Bullet CreateBullet(
            GameObject parent,
            Bullet bulletPrefab,
            float damage,
            float bulletSpeed,
            float bulletLifeTime,
            Vector3 position,
            Vector3 direction)
        {
            Bullet instance = Object.Instantiate(
                bulletPrefab, position, Quaternion.LookRotation(direction)
            );

            CollisionDamageEffect collisionDamageEffect = new CollisionDamageEffect(instance, true, damage);
            instance.Initialize(collisionDamageEffect, bulletSpeed, bulletLifeTime);

            _allBullets.Add(instance);

            void OnInstanceOnDestroyed(MonoDestroyable monoDestroyable)
            {
                _allBullets.Remove(instance);
                monoDestroyable.Destroyed -= OnInstanceOnDestroyed;
            }
            
            instance.Destroyed += OnInstanceOnDestroyed;

            return instance;
        }

        public void ClearBullets()
        {
            for (var index = _allBullets.Count - 1; index >= 0; index--)
            {
                var bullet = _allBullets[index];
                
                _allBullets.RemoveAt(index);
                
                if (bullet.IsDestroyed == false)
                    bullet.Destroy();
            }
        }
    }
}