using Code.Controllers;
using Code.UserInput;
using UnityEngine;

namespace RampJump
{
    public class RootController : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private GameObject _ball;
        [SerializeField] private GameObject _glass;
        [SerializeField] private GameObject _arrow;
        [SerializeField] private GameObject _level;
        [SerializeField] private GameObject _startPlace;

        [SerializeField] private Transform _ballStartPosition;

        private BallTouchHandlingController _ballTouchHandlingController;
        private GlassTouchHandling _glassTouchHandling;
        private ITouchInput _touchInput;
        
        private Controllers _controllers;
        
        private void Start()
        {
            Camera camera = Camera.main;
            
            ITouchInput input = new TouchInputHandling();
            var inputController = new InputController(input);
            
            _ballTouchHandlingController = new BallTouchHandlingController(_ball, _ballStartPosition, _startPlace, camera, input);
            //_glassTouchHandling = new GlassTouchHandling(_glass);

            
            _controllers = new Controllers();
            _controllers.Add(inputController);
            _controllers.Add(_ballTouchHandlingController);
            _controllers.Initialize();
        }

        private void Update()
        {
            _controllers.Execute(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _controllers.FixedExecute(Time.fixedDeltaTime);
        }

        private void OnDestroy()
        {
            _controllers.Cleanup();
        }
    }
}
