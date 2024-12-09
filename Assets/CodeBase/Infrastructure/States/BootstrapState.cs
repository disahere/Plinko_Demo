using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Utils.SmartDebug;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IAssetProvider _assetProvider;

        public BootstrapState(GameStateMachine stateMachine, IAssetProvider assetProvider)
        {
            _stateMachine = stateMachine;
            _assetProvider = assetProvider;
        }

        public void Enter()
        {
            InitializeAssets();
            _stateMachine.Enter<LoadLevelState>();
        }


        public void Exit()
        {
        }

        private void InitializeAssets()
        {
            var hud = _assetProvider.Instantiate(AssetPath.HudPath);
        }
    }
}