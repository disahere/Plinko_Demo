using CodeBase.Utils.SmartDebug;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly string _sceneName;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, string sceneName)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _sceneName = sceneName;
        }

        public void Enter()
        {
            _sceneLoader.Load(_sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
        }
    }
}
