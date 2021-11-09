using UnityEngine;

namespace RampJump
{
    public class BallTouchHandling
    {
        private GameObject _ball;
        private Rigidbody _ballRigidbody;
        private Transform _startPosition;
        private Camera _camera;
        private int _ballID;

        private Vector2 _touchStartPosition;
        private Vector2 _touchDirection;
        private float _force = 100.0f;
        private bool _touchDirectionChosen;

        public BallTouchHandling(GameObject ball, Transform startPosition)
        {
            _ball = ball;
            _ballRigidbody = ball.GetComponentInChildren<Rigidbody>();
            _ballID = _ball.GetComponentInChildren<SphereCollider>().gameObject.GetInstanceID();

            _startPosition = startPosition;
            FixBallPosition();

            _camera = Camera.main;
            _touchDirectionChosen = false;
        }

        public void Execute(float deltaTime)
        {
            if (_touchDirectionChosen == false)
            {
                FixBallPosition();
            }

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Touch touch = Input.GetTouch(0);
            GetTouchDirection(touch);

            if (Physics.Raycast(ray, out hit, 30))
            {
                if (_touchDirectionChosen & hit.collider.gameObject.GetInstanceID() == _ballID)
                {
                    _ballRigidbody.AddForce(
                        new Vector3(_touchDirection.x, _touchDirection.y, _ball.transform.position.z).normalized * _force);
                }
            }
        }

        private void GetTouchDirection(Touch touch)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStartPosition = touch.position;
                    Debug.Log("Begin");
                    break;

                case TouchPhase.Moved:
                    _touchDirection = touch.position - _touchStartPosition;
                    Debug.Log("Moved");
                    break;

                case TouchPhase.Ended:
                    _touchDirectionChosen = true;
                    Debug.Log("End");
                    break;
            }
        }

        private void FixBallPosition()
        {
            _ball.transform.position = _startPosition.position;
            foreach (Transform child in _ball.transform)
            {
                child.transform.position = _startPosition.position;
            }
        }
    }
}
