﻿using System;
using System.Collections.Generic;
using Game.Factories.Ui;
using Game.Models.Combat;
using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;
using SimpleUi.Abstracts;
using UI.Controllers.Abstractions;
using UI.Views.Buffs;
using UI.Views.Combat;
using UI.Views.Stats;
using Zenject;

namespace UI.Controllers
{
    [Serializable]
    public class UnitUIViews
    {
        public AttackButtonView AttackButtonView;
        public ApplyBuffButtonView ApplyBuffButtonView; 
        public ActiveBuffsView ActiveBuffsView;
        public UnitStatsView UnitStatsView;
    }
    
    public class GameUiMainController : UiController<GameUiMainView>, IDisposable
    {
        [Inject] private readonly BattleStateMachine _battleStateMachine;
        
        private readonly IActiveBuffsControllerFactory _activeBuffsControllerFactory;
        private readonly IApplyBuffControllerFactory _applyBuffControllerFactory;
        private readonly IPerformAttackButtonControllerFactory _performAttackButtonControllerFactory;
        private readonly IUnitStatsControllerFactory _unitStatsControllerFactory;
        private readonly List<IInitializableUiController> _uiControllers = new();
        

        public GameUiMainController(IActiveBuffsControllerFactory activeBuffsControllerFactory,
            IApplyBuffControllerFactory applyBuffControllerFactory,
            IPerformAttackButtonControllerFactory performAttackButtonControllerFactory,
            IUnitStatsControllerFactory unitStatsControllerFactory
        )
        {
            _activeBuffsControllerFactory = activeBuffsControllerFactory;
            _applyBuffControllerFactory = applyBuffControllerFactory;
            _performAttackButtonControllerFactory = performAttackButtonControllerFactory;
            _unitStatsControllerFactory = unitStatsControllerFactory;
        }

        public override void OnShow()
        {
            var leftUnit = _battleStateMachine.LeftPlayerSm.Unit;
            var rightUnit = _battleStateMachine.RightPlayerSm.Unit;
            
            CreateControllersForBattleUnit(leftUnit, View.leftUnitUIViews, _battleStateMachine.LeftPlayerSm);
            CreateControllersForBattleUnit(rightUnit, View.rightUnitUIViews, _battleStateMachine.RightPlayerSm);
            
            foreach (var controller in _uiControllers)
            {
                controller.Initialize();
            }
        }

        private void OnCombatStarted(BattleMember leftMember, BattleMember rightMember)
        {
            // var leftViews = View.leftUnitUIViews;
            //
            // CreateControllersForBattleUnit(leftMember);
            // CreateControllersForBattleUnit(rightMember);
            //
            // foreach (var controller in _uiControllers)
            // {
            //     controller.Initialize();
            // }
        }

        private void CreateControllersForBattleUnit(IUnit unit, UnitUIViews views, UnitStateMachine stateMachine)
        {
            //var views = GetViews(battleMember.BattleTeam);
            
            var applyBuffController = _applyBuffControllerFactory.Create(views.ApplyBuffButtonView, unit, stateMachine);
            var attackButton = _performAttackButtonControllerFactory.Create(views.AttackButtonView, unit, stateMachine);
            var statsView = _unitStatsControllerFactory.Create(views.UnitStatsView, unit);
            var activeBuffsController = _activeBuffsControllerFactory.Create(views.ActiveBuffsView, unit);
            
            _uiControllers.Add(applyBuffController);
            _uiControllers.Add(attackButton);
            _uiControllers.Add(statsView);
            _uiControllers.Add(activeBuffsController);
        }

        private UnitUIViews GetViews(EBattleTeam battleTeam)
        {
            return battleTeam switch
            {
                EBattleTeam.Right => View.rightUnitUIViews,
                EBattleTeam.Left => View.leftUnitUIViews,
            };
        }

        public void Dispose()
        {
            foreach (var controller in _uiControllers)
            {
                controller.Dispose();
            }
        }
    }
}