using System;
using Arena.Scripts.Game.Components;
using UnityEngine;

namespace Arena.Scripts.Game
{
    public class Bullet : MonoDestroyable, IPushable
    {
        private float _bulletSpeed;
        private float _bulletLifetime;
        private Rigidbody _rigidbody;
        
        private CollisionDamageEffect _collisionDamageEffect;

        private float _currentAliveTimer;

        public void Initialize(
            CollisionDamageEffect collisionDamageEffect,
            float bulletSpeed,
            float bulletLifetime)
        {
            _rigidbody = GetComponent<Rigidbody>();
         
            _collisionDamageEffect = collisionDamageEffect;
            _bulletSpeed = bulletSpeed;
            _bulletLifetime = bulletLifetime;
        }

        private void Start()
        {
            Push(transform.forward.normalized * _bulletSpeed, Vector3.zero);

            //Destroy(gameObject, _bulletLifetime);
        }
        
        private void OnCollisionEnter(Collision other) => _collisionDamageEffect.OnCollisionEnter(other);

        public void Push(Vector3 force, Vector3 position)
        {
            if (position == Vector3.zero)
                _rigidbody?.AddForce(force, ForceMode.Impulse);
            else
                _rigidbody?.AddForceAtPosition(force, position, ForceMode.Impulse);
        }

        private void Update()
        {
            _currentAliveTimer += Time.deltaTime;

            if (_currentAliveTimer >= _bulletLifetime)
            {
                Destroy();
            }
        }
    }
}