using Core.Systems.Impls;
using Game.Battle.Impl;
using Game.Factories.Buff.Impl;
using Game.Factories.Unit;
using Game.Models.Buffs.Impl;
using Game.Presenters.Unit;
using Game.Presenters.Unit.Impl;
using Game.Services.Buff.Impl;
using Game.Services.BuffFactory.Impl;
using Game.Services.GameField;
using Game.Services.GameField.Impl;
using Game.Systems;
using Game.Views;
using Game.Views.Unit;
using UnityEngine;
using Zenject;

namespace Game.Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameFieldView gameFieldView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Bootstrap>().AsSingle();

            BindGameField();
            BindFactories();
            BindServices();
            BindSystems();
        }

        private void BindFactories()
        {
            Container.BindFactory<DoubleDamageBuff, DoubleDamageBuffFactory>().AsSingle();
            Container.BindFactory<ArmorSelfBuff, ArmorSelfBuffFactory>().AsSingle();
            Container.BindFactory<IUnitView, IUnit, UnitFactory>().To<Unit>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<BuffFactoryProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomBuffService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CombatManager>().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<InitializeGameSystem>().AsSingle();
        }

        private void BindGameField()
        {
            var gameFieldProvider = new GameFieldProvider(gameFieldView);
            Container.Bind<IGameFieldProvider>().To<GameFieldProvider>().FromInstance(gameFieldProvider);
        }
    }
}