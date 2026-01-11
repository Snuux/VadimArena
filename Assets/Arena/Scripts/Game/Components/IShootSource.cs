using System;
using UnityEngine;

namespace Arena.Scripts.Game.Components
{
    public interface IShootSource
    {
        event Action<Vector3> Shooted;
        
        Vector3 Position { get; }
        
        void Shoot(Vector3 direction);
    }
}