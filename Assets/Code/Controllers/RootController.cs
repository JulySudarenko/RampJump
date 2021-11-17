using Code.Configs;
using Code.UniversalFactory;
using Code.UserInput;
using UnityEngine;

namespace Code.Controllers
{
    public class RootController : MonoBehaviour
    {
        [SerializeField] private Data _data;
        private Controllers _controllers;
        
        private void Start()
        {
            var configParser = new ActiveObjectsConfigParser(_data);
            var levelParser = new LevelObjectsConfigParser(_data);
            Camera camera = Camera.main;

            IUserInput input = new UserInputHandling();
            var inputController = new InputController(input);
            
            
            var ballTouchHandlingController = new BallTouchHandlingController(
                configParser.BallObject, configParser.BallSpeed, levelParser.BallStartPosition, 
                levelParser.BallStartPlace, camera, input);
            var glassTouchHandlingController = new GlassTouchHandlingController(
                configParser.GlassObject, configParser.GlassSpeed, levelParser.GlassStartPosition, camera, input);
            
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
