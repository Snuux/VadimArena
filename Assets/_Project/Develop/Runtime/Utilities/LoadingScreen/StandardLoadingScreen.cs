using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.LoadingScreen
{
    public class StandardLoadingScreen : MonoBehaviour, ILoadingScreen
    {
        public bool IsShown => gameObject.activeSelf;

        private void Awake()
        {
            Hide();
            DontDestroyOnLoad(this);
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}