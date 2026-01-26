using UnityEngine;

namespace Assets.Arena.Scripts.Game.Components
{
    public class DiceShootMover
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _pushForce;

        public DiceShootMover(Rigidbody rigidbody, float pushForce)
        {
            _rigidbody = rigidbody;
            _pushForce = pushForce;
        }

        public void PushByDiceValue(Vector3 direction, int topDiceValue)
        {
            float step01 = (topDiceValue - 1f) / 5f;
            float forceMul = Mathf.Lerp(0.4f, 1.0f, step01);
            
            Vector3 side = Vector3.Cross(Vector3.up, direction).normalized;
            float lever = 1f;
            Vector3 position = _rigidbody.worldCenterOfMass + side * lever - Vector3.up * (lever * -1);

            _rigidbody.AddForceAtPosition(direction * (_pushForce * forceMul * -1), position, ForceMode.Impulse);
            
            Quaternion randomRotation = Quaternion.Euler(Random.insideUnitSphere * 90);
            _rigidbody.AddTorque(randomRotation * Quaternion.Euler(0, -90, 0) * direction * (_pushForce * 100), ForceMode.Impulse);
        }
    }
}