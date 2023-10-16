using System.Collections.Generic;
using Core.Systems.Impls;
using Game.Factories.BattleMembers;
using Game.Factories.BattleMembers.Impl;
using Game.Factories.Buffs;
using Game.Factories.Buffs.Impl;
using Game.Factories.StateMachine.Unit;
using Game.Factories.StateMachine.Unit.Impl;
using Game.Factories.States.Battle;
using Game.Factories.States.Battle.Impl;
using Game.Factories.States.Unit;
using Game.Factories.States.Unit.Impl;
using Game.Factories.Ui;
using Game.Factories.Ui.Impl;
using Game.Factories.Unit;
using Game.Factories.Unit.Impl;
using Game.Models.Buffs;
using Game.Models.Combat;
using Game.Presenters.Unit;
using Game.Presenters.Unit.Impl;
using Game.Services.Buffs.Impl;
using Game.Services.GameField;
using Game.Services.GameField.Impl;
using Game.Services.StateMachineInitializer.Battle.Impl;
using Game.Services.StateMachineInitializer.Unit.Impl;
using Game.Settings.Buffs;
using Game.Settings.Unit;
using Game.StateMachine.StateMachine.Impl;
using Game.StateMachine.States.Impl.Battle;
using Game.StateMachine.States.Impl.Unit;
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
            BindStateMachines();

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
                .BindFactoryCustomInterface<IUnitView, List<UnitAttributeParameters>, Unit, UnitFactory, IUnitFactory>()
                .AsSingle();
            
            Container
                .BindFactoryCustomInterface<ApplyBuffButtonView, IUnit, UnitStateMachine, 
                    ApplyBuffController, ApplyBuffControllerFactory, IApplyBuffControllerFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<ActiveBuffsView, IUnit, ActiveBuffsController, 
                    ActiveBuffsControllerFactory, IActiveBuffsControllerFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<AttackButtonView, IUnit, UnitStateMachine,
                    PerformAttackButtonController, PerformAttackButtonControllerFactory, 
                    IPerformAttackButtonControllerFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<UnitStatsView, IUnit, UnitStatsController, 
                    UnitStatsControllerFactory, IUnitStatsControllerFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<BattleStateMachine, BattleStartNewRoundState, 
                    BattleStartNewRoundStateFactory, IBattleStartNewRoundStateFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<BattleStateMachine, BattleWaitForUnitsActionsState, 
                    BattleWaitForUnitsAttackStateFactory, IBattleWaitForUnitsAttackStateFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<IUnit, UnitStateMachine, UnitApplyBuffState, 
                    UnitApplyBuffStateFactory, IUnitApplyBuffStateFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<IUnit, IUnit, UnitStateMachine, UnitAttackState, 
                    UnitAttackStateFactory, IUnitAttackStateFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<IUnit, UnitStateMachine, UnitStartNewRoundState,
                    UnitStartNewRoundStateFactory, IUnitStartNewRoundStateFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<IUnit, UnitStateMachine, UnitWaitForRoundEndState,
                    UnitWaitForRoundEndStateFactory, IUnitWaitForRoundEndStateFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<IUnit, UnitStateMachine, UnitWaitForActionState,
                    UnitWaitForActionStateFactoryFactory, IUnitWaitForActionStateFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<IUnit, UnitStateMachine, UnitWaitForTurnState,
                    UnitWaitForTurnStateFactory, IUnitWaitForTurnStateFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<BattleStateMachine, BattleCompleteState, 
                    BattleCompleteStateFactory, IBattleCompleteStateFactory>().AsSingle();
            
            // Container
            //     .BindFactoryCustomInterface<UnitStateMachine, UnitStateMachine, BattleStateMachine,
            //         BattleStateMachineFactory, IBattleStateMachineFactory>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<IUnit, UnitStateMachine,
                    UnitStateMachineFactory, IUnitStateMachineFactory>().AsSingle();
            
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<RandomBuffProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleStateMachineInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitStateMachineInitializer>().AsSingle();
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

        private void BindStateMachines()
        {
            Container.BindInterfacesAndSelfTo<BattleStateMachine>().AsSingle();
        }
        
    }
}