using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
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