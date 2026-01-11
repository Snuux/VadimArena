namespace Arena.Scripts.Controllers
{
    public abstract class Controller
    {
        private bool _isEnabled;

        public virtual void Enable() => _isEnabled = true;

        public virtual void Disable() => _isEnabled = false;

        public void Update(float deltaTime)
        {
            if (_isEnabled == false)
                return;

            UpdateLogic(deltaTime);
        }
        
        public void FixedUpdate(float deltaTime)
        {
            if (_isEnabled == false)
                return;

            FixedUpdateLogic(deltaTime);
        }

        protected virtual void UpdateLogic(float deltaTime)
        {
        }

        protected virtual void FixedUpdateLogic(float deltaTime)
        {
        }
    }
}