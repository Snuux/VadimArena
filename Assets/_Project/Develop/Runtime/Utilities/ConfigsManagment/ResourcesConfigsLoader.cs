using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs;
using _Project.Develop.Runtime.Utilities.AssetManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.ConfigsManagment
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private ResourcesAssetsLoader _resources;

        private Dictionary<Type, string> _configsResourcesPath = new()
        {
            { typeof(LevelsConfig), "Configs/LevelsConfig" }
        };

        public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();
            
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