using System;
using System.Collections.Generic;
using Game.Battle;
using Game.Factories.Ui;
using Game.Models.Combat;
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
    
    public class GameUiMainController : UiController<GameUiMainView>, IInitializable, IDisposable
    {
        private readonly ICombatManager _combatManager;
        private readonly IActiveBuffsControllerFactory _activeBuffsControllerFactory;
        private readonly IApplyBuffControllerFactory _applyBuffControllerFactory;
        private readonly IPerformAttackButtonControllerFactory _performAttackButtonControllerFactory;
        private readonly IUnitStatsControllerFactory _unitStatsControllerFactory;
        private readonly List<IInitializableUiController> _uiControllers = new();
        

        public GameUiMainController(
            ICombatManager combatManager, 
            IActiveBuffsControllerFactory activeBuffsControllerFactory,
            IApplyBuffControllerFactory applyBuffControllerFactory,
            IPerformAttackButtonControllerFactory performAttackButtonControllerFactory,
            IUnitStatsControllerFactory unitStatsControllerFactory
        )
        {
            _combatManager = combatManager;
            _activeBuffsControllerFactory = activeBuffsControllerFactory;
            _applyBuffControllerFactory = applyBuffControllerFactory;
            _performAttackButtonControllerFactory = performAttackButtonControllerFactory;
            _unitStatsControllerFactory = unitStatsControllerFactory;
        }
        
        public void Initialize()
        {
            _combatManager.BattleStarted += OnCombatStarted;
        }

        private void OnCombatStarted(BattleMember leftMember, BattleMember rightMember)
        {
            CreateControllersForBattleUnit(leftMember);
            CreateControllersForBattleUnit(rightMember);
            
            foreach (var controller in _uiControllers)
            {
                controller.Initialize();
            }
        }

        private void CreateControllersForBattleUnit(BattleMember battleMember)
        {
            var views = GetViews(battleMember.BattleTeam);
            
            var applyBuffController = _applyBuffControllerFactory.Create(views.ApplyBuffButtonView, battleMember.Unit);
            var attackButton = _performAttackButtonControllerFactory.Create(views.AttackButtonView, battleMember.Unit);
            var statsView = _unitStatsControllerFactory.Create(views.UnitStatsView, battleMember.Unit);
            var activeBuffsController = _activeBuffsControllerFactory.Create(views.ActiveBuffsView, battleMember.Unit);
            
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