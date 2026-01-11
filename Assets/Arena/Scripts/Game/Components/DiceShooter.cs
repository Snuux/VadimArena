using System;
using Arena.Scripts.Infrastructure.Spawners;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Arena.Scripts.Game.Components
{
    public class DiceShooter
    {
        private const float SpreadAngleDeg = 30;
        
        public event Action<Vector3> Shooted;

        private readonly BulletFactory _bulletFactory;
        
        private readonly Transform _source;
        private readonly Bullet _bulletPrefab;

        private readonly float _bulletDamage;
        private readonly float _bulletSpeed;
        private readonly float _bulletLifeTime;

        public DiceShooter(Bullet bullet, Transform source, float bulletSpeed, float bulletLifeTime, float bulletDamage, BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
            _bulletDamage = bulletDamage;
            
            _bulletPrefab = bullet;
            _source = source;
            
            _bulletSpeed = bulletSpeed;
            _bulletLifeTime = bulletLifeTime;
        }

        public void Shoot(Vector3 direction, int count)
        {
            SpawnBullet(direction, count);
            Shooted?.Invoke(direction);
        }

        private void SpawnBullet(Vector3 direction, int count)
        {
            if (count <= 1)
            {
                DiceSingleBulletShoot(direction);
                return;
            }

            for (int i = 0; i < count; i++)
            {
                float step = (float)i / (count - 1);
                float delta = Mathf.Lerp(-SpreadAngleDeg, SpreadAngleDeg, step);

                Vector3 bulletDirection = Quaternion.AngleAxis(delta, Vector3.up) * direction;
                bulletDirection.y = 0f;
                bulletDirection.Normalize();

                DiceSingleBulletShoot(bulletDirection);
            }
        }

        private void DiceSingleBulletShoot(Vector3 direction)
        {
            _bulletFactory.CreateBullet(
                _source.gameObject, 
                _bulletPrefab,_bulletDamage, 
                _bulletSpeed, 
                _bulletLifeTime,
                _source.position, 
                direction);
        }
    }
}