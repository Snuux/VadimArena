using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.Infrastructure.Gameplay
{
    public class RandomSymbolsSequenceGenerationService
    {
        private string _symbols;
        private string _sequence;

        public string Symbols => _symbols;
        public string Sequence => _sequence;

        public RandomSymbolsSequenceGenerationService(int length, string symbols)
        {
            Length = length;
            _symbols = symbols;
        }

        public int Length { get; private set; }
        
        public void GenerateSequence()
        {
            _sequence = "";

            for (int i = 0; i < Length; i++)
            {
                char randomSymbol = _symbols[Random.Range(0, _symbols.Length)];
                _sequence += randomSymbol;
            }
        }
    }
}