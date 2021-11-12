using Code.UserInput;
using UnityEngine;

namespace RampJump
{
    public class BallTouchHandlingController : IFixedExecute
    {
        private GameObject _ball;
        private GameObject _startPlace;
        private Transform _startPosition;
        private Camera _camera;
        private float _force = 500.0f;
        
        private Rigidbody _ballRigidbody;
        private int _ballID;
        private Ray _ray;
        private RaycastHit _hit;
        private Vector3 _touchStartPosition;
        private Vector3 _touchDirection;
        private bool _isBallTouched;

        public BallTouchHandlingController(GameObject ball, Transform startPosition, GameObject startPlace, Camera camera, ITouchInput touchInput)
        {
            _ball = ball;
            _startPlace = startPlace;
            _startPosition = startPosition;
            _camera = camera;
            
            _ballRigidbody = ball.GetComponentInChildren<Rigidbody>();
            _ballID = _ball.GetComponentInChildren<SphereCollider>().gameObject.GetInstanceID();
            _ball.transform.position = _startPosition.position;
            
            _isBallTouched = false;
        }

        public void FixedExecute(float deltaTime)
        {
            if (Input.GetMouseButton(0))
            {
                GetDirection();
            }

            if (Input.GetMouseButtonUp(0))
            {
                KickTheBall();
            }
        }

        private void GetDirection()
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
                
            if (Physics.Raycast(_ray, out _hit, 30))
            {
                if (_hit.collider.gameObject.GetInstanceID() == _ballID)
                {
                    _isBallTouched = true;
                    _touchStartPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                        _ball.transform.position.z);
                }
            }
        }

        private void KickTheBall()
        {
            if (_isBallTouched)
            {
                _startPlace.SetActive(false);
                _touchDirection =
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, _ball.transform.position.z);
                _ballRigidbody.AddForce((_touchDirection - _touchStartPosition).normalized * _force);
                _isBallTouched = false;
            }
        }



    }
}
