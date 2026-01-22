using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs   
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/NumbersSequence", fileName = "NumbersSequence")]
    public class NumbersSequence : ScriptableObject, ISequence
    {
        [field: SerializeField] public string Symbols { get; private set; }
        [field: SerializeField] public int Length { get; private set; }
    }
}