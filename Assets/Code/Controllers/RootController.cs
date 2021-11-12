using Code.Configs;
using Code.Factory;
using Code.UserInput;
using UnityEngine;

namespace Code.Controllers
{
    public class RootController : MonoBehaviour
    {
        //[SerializeField] private Data _data;
        [SerializeField] private GameObject _ball;
        [SerializeField] private GameObject _glass;
        [SerializeField] private GameObject _arrow;
        [SerializeField] private GameObject _level;
        [SerializeField] private GameObject _startPlace;

        [SerializeField] private Transform _ballStartPosition;
        [SerializeField] private Transform _glassStartPosition;
        
        private Controllers _controllers;
        
        private void Start()
        {
            //var configParser = new ConfigParser(_data);
            Camera camera = Camera.main;
            
            IUserInput input = new UserInputHandling();
            var inputController = new InputController(input);
            
            var ballTouchHandlingController = new BallTouchHandlingController(_ball, _ballStartPosition, _startPlace, camera, input);
            var glassTouchHandlingController = new GlassTouchHandlingController(_glass, _glassStartPosition, camera, input);
            
            _controllers = new Controllers();
            _controllers.Add(inputController);
            _controllers.Add(ballTouchHandlingController);
            _controllers.Add(glassTouchHandlingController);
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
