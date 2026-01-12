using UnityEngine;

namespace Arena.Scripts.Helpers
{
    public class DestroyWithTarget : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        private void Update()
        {
            if (_target == false)
                Destroy(gameObject);
        }
        
        public void Attach(GameObject targetObject) => _target = targetObject;
    }

}