using System;
using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Start of the project, setup app settings");
            SetupAppSettings();

            Debug.Log("Process of registration of all services");
            DIContainer container = new DIContainer();
            
            EntryPointRegistration.Process(container);
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}