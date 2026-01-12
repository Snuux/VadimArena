using System;
using Arena.Scripts.Game.Components;
using Arena.Scripts.Helpers;
using UnityEngine;

namespace Arena.Scripts.Game
{
    public class Enemy : MonoDestroyable, IPushable, IDamagable
    {
        public event Action<float, float> Damaged
        {
            add => _diceHealth.Damaged += value;
            remove => _diceHealth.Damaged -= value;
        }

        public event Action Died
        {
            add => _diceHealth.Died += value;
            remove => _diceHealth.Died -= value;
        }
        
        private DiceHealth _diceHealth;
        private CollisionDamageEffect _collisionDamageEffect;
        private Rigidbody _rigidbody;
        private float _pushForce;
        
        public bool IsDead => _diceHealth.IsDead;

        public void Initialize(
            DiceHealth diceHealth,
            CollisionDamageEffect collisionDamageEffect,
            float pushForce,
            float maxLinearVelocity,
            float maxAngularVelocity)
        {
            _diceHealth = diceHealth;
            _collisionDamageEffect = collisionDamageEffect;
            _pushForce = pushForce;
            
            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.maxAngularVelocity = maxAngularVelocity;
            _rigidbody.maxLinearVelocity = maxLinearVelocity;
            
            Died += Destroy;
        }
        
        private void OnCollisionEnter(Collision other) => _collisionDamageEffect.OnCollisionEnter(other);

        public void Push(Vector3 direction, Vector3 position) => 
            _rigidbody.AddForce(direction * _pushForce, ForceMode.Impulse);

        public void TakeDamage(float damage) => _diceHealth.TakeDamage(damage);
    }
}