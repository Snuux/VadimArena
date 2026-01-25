using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Infrastructure.ConfigsManagment;
using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Infrastructure.Meta.Infrastructure;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Meta.Infrastructure
{
    public partial class MainMenuBootstrap : SceneBoostrap
    {
        private DIContainer _container;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Initialization of meta scene");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start of meta scene");
            Debug.Log("Enter 1 or 2 for load sequence with Letters or Digits:");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _container.Resolve<ChangeSceneByLevelTypeService>().ChangeSceneBy(LevelTypes.Letters);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                _container.Resolve<ChangeSceneByLevelTypeService>().ChangeSceneBy(LevelTypes.Digits);
        }
    }
}