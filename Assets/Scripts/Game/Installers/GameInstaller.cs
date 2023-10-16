using Core.Systems.Impls;
using Game.Battle.Impl;
using Game.Factories.BattleMembers;
using Game.Factories.BattleMembers.Impl;
using Game.Factories.Buffs;
using Game.Factories.Buffs.Impl;
using Game.Factories.Ui;
using Game.Factories.Ui.Impl;
using Game.Factories.Unit;
using Game.Factories.Unit.Impl;
using Game.Models.Buffs;
using Game.Models.Combat;
using Game.Presenters.Unit;
using Game.Presenters.Unit.Impl;
using Game.Services.AttackManager.Impl;
using Game.Services.AttackQueueProvider.Impl;
using Game.Services.Buffs.Impl;
using Game.Services.GameField;
using Game.Services.GameField.Impl;
using Game.Services.Round.Impl;
using Game.Services.TargetService;
using Game.Services.TargetService.Impl;
using Game.Settings.Buffs;
using Game.Systems;
using Game.Views;
using Game.Views.Unit;
using UI.Controllers.Combat;
using UI.Controllers.Stats;
using UI.Views.Buffs;
using UI.Views.Combat;
using UI.Views.Stats;
using UI.Windows;
using UnityEngine;
using Zenject;

namespace Game.Installers
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

            Container.BindInterfacesAndSelfTo<GameHudWindow>().AsSingle();
        }

        private void BindFactories()
        {
            Container
                .BindFactoryCustomInterface<IBuffsSettingsBase, string, Buff, BuffFactory, IBuffFactory>()
                .AsSingle();
            
            Container
                .BindFactoryCustomInterface<IUnit, EBattleTeam, int, BattleMember, BattleMemberFactory, IBattleMemberFactory>()
                .AsSingle();
            
            Container
                .BindFactoryCustomInterface<IUnitView, Unit, UnitFactory, IUnitFactory>()
                .AsSingle();
            
            Container
                .BindFactoryCustomInterface<ApplyBuffButtonView, IUnit, 
                    ApplyBuffController, ApplyBuffControllerFactory, IApplyBuffControllerFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<ActiveBuffsView, IUnit, ActiveBuffsController, 
                    ActiveBuffsControllerFactory, IActiveBuffsControllerFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<AttackButtonView, IUnit,
                    PerformAttackButtonController, PerformAttackButtonControllerFactory, 
                    IPerformAttackButtonControllerFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<UnitStatsView, IUnit, UnitStatsController, 
                    UnitStatsControllerFactory, IUnitStatsControllerFactory>().AsSingle();
            
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<RandomBuffProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<CombatManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<RoundProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackQueueService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackManager>().AsSingle();
            
            Container.Bind<ITargetService>().To<СircularTargetService>().AsSingle();
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