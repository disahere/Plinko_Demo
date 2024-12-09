using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, IAssetProvider assetProvider, SceneLoader sceneLoader, DiContainer container)
        {
            StateMachine = new GameStateMachine(container);
            
            StateMachine.RegisterState(new BootstrapState(StateMachine, assetProvider));
            StateMachine.RegisterState(new LoadLevelState(StateMachine, sceneLoader, "GameScene"));
            StateMachine.RegisterState(container.Instantiate<GameLoopState>());
        }
    }
}