using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.AssetManagment
{
    public class ResourcesAssetsLoader
    {
        public T Load<T>(string resourcePath) where T : Object
        => Resources.Load<T>(resourcePath);
    }
}