using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Gameplay.Infrastructure
{
    public class GameplayContextRegistration
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Process of services registration in gameplay scene");
        }
    }
}