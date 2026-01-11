using UnityEngine;

namespace Arena.Scripts.Game.Components
{
    public class CollisionDamageEffect
    {
        private readonly MonoDestroyable _source;
        private readonly bool _destroyOnHit;

        public float Damage { get; }

        public CollisionDamageEffect(MonoDestroyable source, bool destroyOnHit, float damage)
        {
            _source = source;
            _destroyOnHit = destroyOnHit;
            Damage = damage;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.TakeDamage(Damage);

                if (_destroyOnHit)
                    _source.Destroy();
            }
        }
    }
}