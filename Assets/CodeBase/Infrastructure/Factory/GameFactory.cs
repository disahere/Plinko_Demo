using CodeBase.Infrastructure.AssetManagement;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public void CreateHud() =>
            _assets.Instantiate(AssetPath.HudPath);
    }
}