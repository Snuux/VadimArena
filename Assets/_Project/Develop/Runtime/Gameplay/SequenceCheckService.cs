using System;

namespace _Project.Develop.Runtime.Gameplay
{
    public class SequenceCheckService
    {
        public bool IsSame(string sequenceTarget, string sequenceSource)
        {
            if (sequenceSource.Length != sequenceTarget.Length || sequenceTarget.Length == 0)
                throw new InvalidOperationException("Wrong length of sequence!");

            if (sequenceSource.Length == 0)
                throw new InvalidOperationException("Not generated sequence! Please generate first");

            return sequenceTarget.Equals(sequenceSource);
        }
    }
}