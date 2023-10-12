using Game.Services.SceneLoading.Impl;
using Zenject;

namespace Project.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneLoadingManager>().AsSingle();
        }
    }
}