using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.Infrastructure.Gameplay
{
    public class RandomSymbolsSequenceService
    {
        private string _symbols;
        private string _randomSequence;

        public string Symbols => _symbols;
        public string RandomSequence => _randomSequence;

        public RandomSymbolsSequenceService(int length, string symbols)
        {
            Length = length;
            _symbols = symbols;
        }

        public int Length { get; private set; }
        
        public void GenerateSequence()
        {
            _randomSequence = "";

            for (int i = 0; i < Length; i++)
            {
                char randomSymbol = _symbols[Random.Range(0, _symbols.Length)];
                _randomSequence += randomSymbol;
            }
        }

        public bool IsSameSequence(string sequence)
        {
            if (_randomSequence.Length != sequence.Length || sequence.Length == 0)
                throw new InvalidOperationException("Wrong length of sequence!");

            if (_randomSequence.Length == 0)
                throw new InvalidOperationException("Not generated sequence! Please generate first");

            return sequence.Equals(_randomSequence);
        }
    }
}