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
        private ChangeSceneByLevelTypeService _changeSceneByLevelTypeService;
        
        private bool _running;

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Initialization of meta scene");

            _changeSceneByLevelTypeService = _container.Resolve<ChangeSceneByLevelTypeService>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start of meta scene");
            Debug.Log("Enter 1 or 2 for load sequence with Letters or Digits:");

            _running = true;
        }

        public void Update()
        {
            if (_running)
                _changeSceneByLevelTypeService.Update(Time.deltaTime);
        }
    }
}