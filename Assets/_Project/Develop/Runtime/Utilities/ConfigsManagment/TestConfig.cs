using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.ConfigsManagment
{
    [CreateAssetMenu(menuName = "ConfigsManagment/TestConfig", fileName = "TestConfig")]
    public class TestConfig : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
    }
}