using System.Collections;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure
{
    public abstract class SceneBoostrap : MonoBehaviour
    {
        public abstract void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null);

        public abstract IEnumerator Initialize();

        public abstract void Run();
    }
}