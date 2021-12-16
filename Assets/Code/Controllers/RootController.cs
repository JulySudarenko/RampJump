using Code.Configs;
using Code.GameState;
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
        [SerializeField] private AudioSource _rampSource;
        [SerializeField] private Transform _particle;
        private Controllers _controllers;

        private void Start()
        {
            var configParser = new ActiveObjectsConfigParser(_data.ActiveObjectConfig);
            Camera camera = Camera.main;

            IUserInput input = new UserInputHandling();
            var inputController = new InputController(input);

            var ballTouchHandlingController = new BallTouchController(configParser.BallModel,
                _data.ActiveObjectConfig, camera, input);
            var arrowController = new ArrowController(configParser.ArrowObject, input, configParser.BallModel.Ball);
            var gameplayController = new GameplayController(configParser.HoleObject, configParser.BallModel.Ball,
                configParser.BallModel.BallID);
            var levelController = new LevelController(_endGameView, _data.LevelObjectConfig, configParser.BallModel,
                configParser.HoleObject, configParser.ArrowObject);
            var effectController = new EffectController(configParser.BallModel.Ball, _particle);

            var coins = new CoinsController(levelController.CoinsList, configParser.BallModel.AudioSource,
                configParser.CoinSound);
            var slimeCheckout = new SlimeCheckout(levelController.ComponentsList, _rampSource,
                configParser.BallSlimeSound, configParser.BallModel.BallHit);

            var gameStateController = new StateController(ballTouchHandlingController, arrowController,
                gameplayController, levelController);

            _controllers = new Controllers();
            _controllers.Add(inputController);
            _controllers.Add(ballTouchHandlingController);
            _controllers.Add(arrowController);
            _controllers.Add(gameplayController);
            _controllers.Add(levelController);
            //_controllers.Add(effectController);
            _controllers.Add(coins);
            _controllers.Add(slimeCheckout);
            _controllers.Add(gameStateController);
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
