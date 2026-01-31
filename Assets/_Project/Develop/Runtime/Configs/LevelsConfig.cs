using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelsConfig", fileName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private List<Config> _configs;

        public LevelConfig GetLevelConfigBy(LevelTypes levelType) 
            => _configs.First(config => config.LevelType == levelType).LevelConfig;

        [Serializable]
        private class Config
        {
            [field: SerializeField] public LevelTypes LevelType { get; private set; }
            [field: SerializeField] public LevelConfig LevelConfig { get; private set; }
        }
    }
}