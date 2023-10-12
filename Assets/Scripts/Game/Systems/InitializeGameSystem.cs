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
        private readonly BattleManager _battleManager;

        public InitializeGameSystem(
            IGameFieldProvider gameFieldProvider, 
            UnitFactory unitFactory
        )
        {
            _gameFieldProvider = gameFieldProvider;
            _unitFactory = unitFactory;
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
            
            _battleManager.StartBattle(battleUnits);
        }
    }
}