using System.Linq;
using UnityEngine;

namespace Arena.Scripts.Game.Components
{
    public class DiceNumberCalculator
    {
        private readonly Transform _transform;

        public DiceNumberCalculator(Transform transform)
        {
            _transform = transform;
        }

        public int GetTopValue()
        {
            var candidates = new (float dot, int value)[]
            {
                (Vector3.Dot( _transform.up,      Vector3.up), 1), // вверх -> 1
                (Vector3.Dot(-_transform.up,      Vector3.up), 6), // вниз  -> 6

                (Vector3.Dot( _transform.forward, Vector3.up), 2), // вперёд -> 2
                (Vector3.Dot(-_transform.forward, Vector3.up), 5), // назад  -> 5

                (Vector3.Dot( _transform.right,   Vector3.up), 4), // вправо -> 4
                (Vector3.Dot(-_transform.right,   Vector3.up), 3), // влево  -> 3
            };
            
            return candidates.OrderByDescending(c => c.dot).First().value;
        }
    }
}