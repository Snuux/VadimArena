using UnityEngine;

namespace Arena.Scripts.Game.Components
{
    public interface IPushable
    {
        void Push(Vector3 force, Vector3 position);
    }
}