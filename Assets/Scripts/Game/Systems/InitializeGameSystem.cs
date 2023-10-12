using System.Collections.Generic;
using Core.Systems;
using Game.Battle;
using Game.Factories.Unit;
using Game.Presenters.Unit;
using Game.Services.GameField;

namespace Game.Systems
{
    public class InitializeGameSystem : IInitializeSystem
    {
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly UnitFactory _unitFactory;
        private readonly ICombatManager _combatManager;

        public InitializeGameSystem(
            IGameFieldProvider gameFieldProvider, 
            ICombatManager combatManager,
            UnitFactory unitFactory
        )
        {
            _gameFieldProvider = gameFieldProvider;
            _unitFactory = unitFactory;
            _combatManager = combatManager;
        }
        
        public void Initialize()
        {
            var gameField = _gameFieldProvider.GameField;

            var battleUnits = new List<IUnit>();

            foreach (var unitView in gameField.UnitsViews)
            {
                var unit = _unitFactory.Create(unitView);
                
                battleUnits.Add(unit);
            }
            
            _combatManager.StartCombat(battleUnits);
        }
    }
}