using Assets.Arena.Scripts.Game.Components;
using UnityEngine;

namespace Assets.Arena.Scripts.Controllers
{
    public class ShootPushableController : Controller
    {
        private readonly IPushable _pushable;
        private readonly IShootSource _shootSource;

        public ShootPushableController(IPushable pushable, IShootSource shootSource)
        {
            _pushable = pushable;
            _shootSource = shootSource;
        }

        private void Move(Vector3 direction)
        {
            _pushable.Push(direction, Vector3.zero);
        }
    
        public override void Enable()
        {
            base.Enable();
        
            _shootSource.Shooted += Move;
        }

        public override void Disable()
        {
            base.Disable();
        
            _shootSource.Shooted -= Move;
        }
    }
}

