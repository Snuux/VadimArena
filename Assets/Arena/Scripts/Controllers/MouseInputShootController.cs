using Assets.Arena.Scripts.Game.Components;
using UnityEngine;

namespace Assets.Arena.Scripts.Controllers
{
    public class MouseInputShootController : Controller
    {
        private readonly IShootSource _shootSource;
        private readonly Camera _camera;

        public MouseInputShootController(IShootSource shootSource)
        {
            _shootSource = shootSource;
            _camera = Camera.main;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
                if (TryGetMouseDirection(out Vector3 direction))
                    _shootSource.Shoot(direction);
        }

        private bool TryGetMouseDirection(out Vector3 direction)
        {
            Plane shootPlane = new Plane(Vector3.up, new Vector3(0f, _shootSource.Position.y, 0f));
            Ray mouseRay = _camera.ScreenPointToRay(Input.mousePosition);

            if (shootPlane.Raycast(mouseRay, out float enter))
            {
                Vector3 mouseWorld = mouseRay.GetPoint(enter);

                direction = mouseWorld - _shootSource.Position;
                direction.y = 0f;
                direction = direction.normalized;

                return true;
            }

            direction = Vector3.zero;
            return false;
        }
    }
}