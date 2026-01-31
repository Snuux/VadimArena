using System.Collections.Generic;

namespace _Project.Develop.Runtime.Utilities.SceneManagment
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(int length, string symbols)
        {
            Length = length;
            Symbols = symbols;
        }

        public int Length { get; }
        public string Symbols { get; }
    }
}