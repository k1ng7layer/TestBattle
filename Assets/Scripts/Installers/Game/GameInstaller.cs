using Core.Systems.Impls;
using Game.Factories.Buff;
using Game.Factories.Buff.Impl;
using Game.Factories.Unit;
using Game.Models.Buffs.Impl;
using Game.Presenters.Unit;
using Game.Presenters.Unit.Impl;
using Game.Services.Buff.Impl;
using Game.Services.BuffFactory.Impl;
using Game.Systems;
using Game.Views.Unit;
using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Bootstrap>().AsSingle();

            BindFactories();
            BindServices();
            BindSystems();
        }

        private void BindFactories()
        {
            Container.BindFactoryCustomInterface<DoubleDamageBuff, DoubleDamageBuffFactory, IBuffFactory>().AsSingle();
            Container.BindFactoryCustomInterface<ArmorSelfBuff, ArmorSelfBuffFactory, IBuffFactory>().AsSingle();
            Container.BindFactory<IUnitView, IUnit, UnitFactory>().To<Unit>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<BuffFactoryProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomBuffService>().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<InitializeGameSystem>().AsSingle();
        }
    }
}