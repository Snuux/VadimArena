using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs   
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/SymbolsSequence", fileName = "SymbolsSequence")]
    public class SymbolsSequence : ScriptableObject, ISequence
    {
        [field: SerializeField] public string Symbols { get; private set; }
        [field: SerializeField] public int Length { get; private set; }
    }
}