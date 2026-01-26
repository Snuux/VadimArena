using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure.Meta.Infrastructure
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