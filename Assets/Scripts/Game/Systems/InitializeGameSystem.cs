using System.Collections.Generic;
using Core.Systems;
using Game.Battle;
using Game.Factories.BattleMembers.Impl;
using Game.Factories.Unit;
using Game.Models.Combat;
using Game.Presenters.Unit;
using Game.Services.GameField;
using SimpleUi.Signals;
using UI.Windows;
using Zenject;

namespace Game.Systems
{
    public class InitializeGameSystem : IInitializeSystem
    {
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly ICombatManager _combatManager;
        private readonly IUnitFactory _unitFactory;
        private readonly IBattleMemberFactory _battleMemberFactory;
        private readonly DiContainer _diContainer;
        private readonly SignalBus _signalBus;

        public InitializeGameSystem(
            IGameFieldProvider gameFieldProvider, 
            ICombatManager combatManager,
            IUnitFactory unitFactory,
            IBattleMemberFactory battleMemberFactory,
            DiContainer diContainer, 
            SignalBus signalBus
        )
        {
            _gameFieldProvider = gameFieldProvider;
            _unitFactory = unitFactory;
            _battleMemberFactory = battleMemberFactory;
            _diContainer = diContainer;
            _signalBus = signalBus;
            _combatManager = combatManager;
        }
        
        public void Initialize()
        {
            var gameField = _gameFieldProvider.GameField;
            var battleMembers = new List<BattleMember>();
            
            for (int i = 0; i < gameField.UnitsViews.Count; i++)
            {
                var unitSettings = gameField.UnitsViews[i];
                
                var unit = _unitFactory.Create(unitSettings.View);

                InitializeUI(unit);
                
                var battleMember = _battleMemberFactory.Create(unit, unitSettings.BattleTeam, unitSettings.Order);
                    
                battleMembers.Add(battleMember);
            }
            
            _combatManager.StartCombat(battleMembers);
            
            _signalBus.OpenWindow<GameHudWindow>();
        }

        private void InitializeUI(IUnit unit)
        {
            // var statController = new UnitStatsController();
            //
            // _diContainer.Inject(statController);
            //
            // statController.AttachUnit(unit);
        }
    }
}