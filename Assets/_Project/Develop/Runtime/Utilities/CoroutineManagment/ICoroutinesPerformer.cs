using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.CoroutineManagment
{
    public interface ICoroutinesPerformer
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);
        void StopPerform(Coroutine coroutine);
    }
}