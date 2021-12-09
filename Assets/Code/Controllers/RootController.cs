using Code.Configs;
using Code.LevelConstructor;
using Code.UserInput;
using Code.View;
using UnityEngine;

namespace Code.Controllers
{
    public class RootController : MonoBehaviour
    {
        [SerializeField] private Data _data;
        [SerializeField] private EndGameView _endGameView;
        [SerializeField] private Transform _particle;
        private Controllers _controllers;

        private void Start()
        {
            var configParser = new ActiveObjectsConfigParser(_data.ActiveObjectConfig);
            Camera camera = Camera.main;

            IUserInput input = new UserInputHandling();
            var inputController = new InputController(input);

            var ballTouchHandlingController = new BallTouchHandlingController(configParser.BallModel,
                _data.ActiveObjectConfig, camera, input);
            var arrowController = new ArrowController(ballTouchHandlingController, configParser.ArrowObject, input,
                configParser.BallModel.Ball);

            var gameplayController = new GameplayController(ballTouchHandlingController, configParser.HoleObject,
                configParser.BallModel);
            var levelController = new LevelController(gameplayController, ballTouchHandlingController, _endGameView,
                _data.LevelObjectConfig, configParser.BallModel, configParser.HoleObject, configParser.ArrowObject);
            var effectController = new EffectController(configParser.BallModel.Ball, _particle);
            var coins = new CoinsController(levelController.CoinsList);
            
            _controllers = new Controllers();
            _controllers.Add(inputController);
            _controllers.Add(ballTouchHandlingController);
            _controllers.Add(arrowController);
            _controllers.Add(gameplayController);
            _controllers.Add(levelController);
            _controllers.Add(effectController);
            _controllers.Add(coins);
            _controllers.Initialize();
        }

        private void Update()
        {
            _controllers.Execute(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _controllers.Cleanup();
        }
    }
}
