using System;
using System.Collections;
using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Meta.Infrastructure
{
    public class MainMenuBoostrap : SceneBoostrap
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
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
                
                coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(
                    Scenes.Gameplay, new GameplayInputArgs(4)));
            }
        }
    }
}