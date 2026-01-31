using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Process of services registration in menu scene");

            container.RegisterAsSingle(CreateChangeSceneByLevelTypeService);
        }

        private static ChangeSceneByLevelTypeService CreateChangeSceneByLevelTypeService(DIContainer c)
        {
            return new ChangeSceneByLevelTypeService(c);
        }
    }
}