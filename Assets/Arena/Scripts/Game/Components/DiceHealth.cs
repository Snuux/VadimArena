using System;
using UnityEngine;

namespace Assets.Arena.Scripts.Game.Components
{
    public class DiceHealth
    {
        public event Action<float, float> Damaged;
        public event Action Died;
        
        private float _value;

        public bool IsDead => Value <= 0f; 

        public DiceHealth(float initHealth)
        {
            Value = initHealth;
        }

        public float Value
        {
            get => _value;
            
            private set
            {
                float healthBefore = _value;
                
                _value = Math.Max(value, 0);
                
                if (healthBefore > 0f && _value <= 0f)
                    Die();
            }
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                throw new System.Exception("Damage must be greater than or equal to 0");
            
            if (damage == 0) 
                return;
            
            float rawDamage = damage;
            float receivedDamage = Mathf.Min(rawDamage, Value);
            
            Value -= damage;
            
            Damaged?.Invoke(rawDamage, receivedDamage);
        }

        private void Die()
        {
            Died?.Invoke();
        }
    }
}