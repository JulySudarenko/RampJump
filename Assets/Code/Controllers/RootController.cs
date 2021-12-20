﻿using Code.Assistant;
using Code.Configs;
using Code.GameState;
using Code.LevelConstructor;
using Code.Timer;
using Code.UserInput;
using Code.View;
using UnityEngine;

namespace Code.Controllers
{
    public class RootController : MonoBehaviour
    {
        [SerializeField] private Data _data;
        [SerializeField] private EndGameView _endGameView;
        private Controllers _controllers;

        private void Start()
        {
            var configParser = new ActiveObjectsConfigParser(_data.ActiveObjectConfig);
            Camera camera = Camera.main;
            var cameraAudioSource = camera.gameObject.GetOrAddComponent<AudioSource>();
            
            IUserInput input = new UserInputHandling();
            var inputController = new InputController(input);

            var ballTouchHandlingController = new BallTouchController(configParser.Ball,
                _data.ActiveObjectConfig, camera, input, cameraAudioSource, _data.ActiveObjectConfig.KickSound);
            var arrowController = new ArrowController(configParser.ArrowObject, input, configParser.Ball.BallTransform,
                _data.ActiveObjectConfig, _data.ActiveObjectConfig.ArrowColors);
            var gameplayController = new GameplayController(configParser.HoleObject, configParser.Ball.BallTransform,
                configParser.Ball.BallID, cameraAudioSource, _data.ActiveObjectConfig.DirectHitSound);
            var levelController = new LevelController(_endGameView, _data.LevelObjectConfig, configParser.Ball,
                configParser.HoleObject, configParser.ArrowObject);

            var coins = new CoinsController(levelController.CoinsList, configParser.Ball.BallAudioSource,
                configParser.CoinSound, _data.ActiveObjectConfig.CoinsRotationAngle, _data.ActiveObjectConfig.CoinsRotationSpeed);
            var slimeCheckout = new SlimeCheckout(levelController.ComponentsList, cameraAudioSource,
                configParser.BallSlimeSound, configParser.Ball.BallHit);

            var gameStateController = new StateController(ballTouchHandlingController, arrowController,
                gameplayController, levelController);

            _controllers = new Controllers();
            _controllers.Add(inputController);
            _controllers.Add(new TimeRemainingController());
            _controllers.Add(ballTouchHandlingController);
            _controllers.Add(arrowController);
            _controllers.Add(gameplayController);
            _controllers.Add(levelController);
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
