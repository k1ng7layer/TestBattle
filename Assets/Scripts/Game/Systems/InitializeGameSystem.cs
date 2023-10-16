using Core.Systems;
using Game.Factories.StateMachine.Unit;
using Game.Factories.Unit;
using Game.Services.GameField;
using Game.Services.StateMachineInitializer.Battle;
using Game.Services.StateMachineInitializer.Unit;
using Game.StateMachine.StateMachine.Impl;
using Game.StateMachine.States;
using SimpleUi.Signals;
using UI.Windows;
using Zenject;

namespace Game.Systems
{
    public class InitializeGameSystem : IInitializeSystem
    {
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IUnitFactory _unitFactory;
        private readonly IUnitStateMachineFactory _unitStateMachineFactory;
        private readonly IUnitStateMachineInitializer _unitStateMachineInitializer;
        private readonly IBattleStateMachineInitializer _battleStateMachineInitializer;
        private readonly BattleStateMachine _battleStateMachine;

        private readonly SignalBus _signalBus;

        public InitializeGameSystem(
            IGameFieldProvider gameFieldProvider,
            IUnitFactory unitFactory,
            IUnitStateMachineFactory unitStateMachineFactory,
            IUnitStateMachineInitializer unitStateMachineInitializer,
            IBattleStateMachineInitializer battleStateMachineInitializer,
            BattleStateMachine battleStateMachine,
            SignalBus signalBus
        )
        {
            _gameFieldProvider = gameFieldProvider;
            _unitFactory = unitFactory;
            _unitStateMachineFactory = unitStateMachineFactory;
            _unitStateMachineInitializer = unitStateMachineInitializer;
            _battleStateMachineInitializer = battleStateMachineInitializer;
            _battleStateMachine = battleStateMachine;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            var gameField = _gameFieldProvider.GameField;
            
            var rightUnit = _unitFactory.Create(gameField.RightUnitSettings.View, gameField.RightUnitSettings.Parameters);
            var leftUnit = _unitFactory.Create(gameField.LeftUnitSettings.View, gameField.LeftUnitSettings.Parameters);
            
            var rightUnitStateMachine = _unitStateMachineFactory.Create(rightUnit);
            var leftUnitStateMachine = _unitStateMachineFactory.Create(leftUnit);
            
            _unitStateMachineInitializer.InitializeStates(rightUnit, leftUnit, rightUnitStateMachine);
            _unitStateMachineInitializer.InitializeStates(leftUnit, rightUnit, leftUnitStateMachine);
            
            _battleStateMachineInitializer.InitializeStates(_battleStateMachine);
            _battleStateMachine.Initialize(leftUnitStateMachine, rightUnitStateMachine);
            _battleStateMachine.ChangeState(EBattleState.StartNewRound);
            
            _signalBus.OpenWindow<GameHudWindow>();
        }
        
    }
}