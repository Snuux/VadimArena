using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Infrastructure.Gameplay.Infrastructure
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