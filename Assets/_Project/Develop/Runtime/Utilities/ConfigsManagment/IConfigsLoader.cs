using System;
using System.Collections;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Infrastructure.ConfigsManagment
{
    public interface IConfigsLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded);
    }
}