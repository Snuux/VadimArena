using System;
using Assets.Arena.Scripts.Game.Components;
using Assets.Arena.Scripts.Helpers;
using UnityEngine;

namespace Assets.Arena.Scripts.Game
{
    public class Hero : MonoDestroyable, IShootSource, IPushable, IDamagable
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

        public event Action<Vector3> Shooted
        {
            add => _diceShooter.Shooted += value;
            remove => _diceShooter.Shooted -= value;
        }

        private DiceShooter _diceShooter;
        private DiceNumberCalculator _diceNumberCalculator;
        private DiceShootMover _diceShootMover;
        private DiceHealth _diceHealth;
        private CollisionDamageEffect _collisionDamageEffect;

        public Vector3 Position => transform.position;
        public bool IsDead => _diceHealth.IsDead;
        public float Health => _diceHealth.Value;

        public void Initialize(
            DiceShooter diceShooter,
            DiceNumberCalculator diceNumberCalculator,
            DiceShootMover diceShootMover,
            DiceHealth diceHealth,
            CollisionDamageEffect collisionDamageEffect,
            float maxLinearVelocity,
            float maxAngularVelocity)
        {
            _diceShooter = diceShooter;
            _diceNumberCalculator = diceNumberCalculator;
            _diceShootMover = diceShootMover;
            _diceHealth = diceHealth;
            _collisionDamageEffect = collisionDamageEffect;

            Rigidbody rigidbody = GetComponent<Rigidbody>();

            rigidbody.maxAngularVelocity = maxAngularVelocity;
            rigidbody.maxLinearVelocity = maxLinearVelocity;

            Died += Destroy;
        }
        
        private void OnCollisionEnter(Collision other) => _collisionDamageEffect.OnCollisionEnter(other);

        public void Shoot(Vector3 direction)
        {
            int diceTopNumber = _diceNumberCalculator.GetTopValue();
            _diceShooter?.Shoot(direction, diceTopNumber);
        }

        public void Push(Vector3 direction, Vector3 position) => 
            _diceShootMover.PushByDiceValue(direction, _diceNumberCalculator.GetTopValue());

        public void TakeDamage(float damage) => _diceHealth.TakeDamage(damage);
    }
}