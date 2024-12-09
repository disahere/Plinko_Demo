using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Utils.SmartDebug;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private readonly DSender _sender = new("GameBootstrapper");
        public LoadingCurtain Curtain;

        private Game _game;
        private DiContainer _container;

        [Inject]
        private IAssetProvider _assetProvider;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);

            DLogger.Message(_sender)
                .WithText("GameBootstrapper initialized with AssetProvider")
                .Log();
            
            _game = new Game(
                this,
                Curtain,
                _container.Resolve<IAssetProvider>(),
                _container.Resolve<SceneLoader>(),
                _container
            );
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}