using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Infrastructure.AssetManagment;
using Assets._Project.Develop.Runtime.Configs;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.ConfigsManagment
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private ResourcesAssetsLoader _resources;

        private Dictionary<Type, string> _configsResourcesPath = new()
        {
            { typeof(SymbolsSequence), "Configs/CharactersSequence" },
            { typeof(NumbersSequence), "Configs/NumbersSequence" }
        };

        public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();
            
            //для каждого типа мы подгружаем конфиг по пути
            foreach (KeyValuePair<Type,string> configResourcesPath in _configsResourcesPath)
            {
                ScriptableObject config = _resources
                    .Load<ScriptableObject>(configResourcesPath.Value);
                
                loadedConfigs.Add(configResourcesPath.Key, config);
                yield return null;
            }
            
            onConfigsLoaded?.Invoke(loadedConfigs);
        }
    }
}