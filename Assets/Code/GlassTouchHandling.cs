using UnityEngine;

namespace RampJump
{
    public class GlassTouchHandling
    {
        private GameObject _glass;
        private Camera _camera;
        private int _glassID;

        private Vector3 _newPosition;
        private Vector2 _touchStartPosition;
        private Vector2 _touchDirection;
        private float _speed = 0.25f;
        private bool _touchDirectionChosen;

        public GlassTouchHandling(GameObject glass)
        {
            _glass = glass;
            _camera = Camera.main;
            _glassID = _glass.GetComponentInChildren<CapsuleCollider>().gameObject.GetInstanceID();
            Debug.Log(_glassID);
        }

        public void Execute(float deltaTime)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Touch touch = Input.GetTouch(0);
            GetTouchDirection(touch);
            
            if (Physics.Raycast(ray, out hit, 20))
            {
                Debug.Log($"Fix touching {hit.collider.gameObject.name}" +
                          $" {hit.collider.gameObject.GetInstanceID()}");
                if (hit.collider.gameObject.GetInstanceID() == _glassID)
                {
                    _newPosition = _glass.transform.position + new Vector3(
                        (touch.position - _touchStartPosition).x,
                        _glass.transform.position.y, _glass.transform.position.z);
                    _glass.transform.position =
                        Vector3.Lerp(_glass.transform.position, _newPosition, deltaTime * _speed);
                }
            }
        }
        
        private void GetTouchDirection(Touch touch)
        {

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStartPosition = touch.position;
                    //_touchDirectionChosen = false;
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
    }
}
