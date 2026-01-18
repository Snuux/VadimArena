using System.Collections;
using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Meta.Infrastructure
{
    public class MetaBoostrap : SceneBoostrap
    {
        private DIContainer _container;
        
        public override IEnumerator Initialize(DIContainer container)
        {
            _container = container;
            
            Debug.Log("Initialization of meta scene");
            
            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start of meta scene");
        }
    }
}