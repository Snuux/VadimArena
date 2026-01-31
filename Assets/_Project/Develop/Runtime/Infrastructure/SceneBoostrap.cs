using System.Collections;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure
{
    public abstract class SceneBoostrap : MonoBehaviour
    {
        public abstract void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null);

        public abstract IEnumerator Initialize();

        public abstract void Run();
    }
}