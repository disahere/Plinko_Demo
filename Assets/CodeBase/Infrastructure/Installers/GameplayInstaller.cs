using CodeBase.Infrastructure.AssetManagement;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(FindObjectOfType<GameBootstrapper>()).AsSingle();
        }
    }
}