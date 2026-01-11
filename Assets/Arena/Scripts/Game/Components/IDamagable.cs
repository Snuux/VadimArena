using System;

namespace Arena.Scripts.Game.Components
{
    public interface IDamagable
    {
        event Action<float, float> Damaged; //нанесенный, полученный урон
        event Action Died;
        
        void TakeDamage(float damage);
    }
}