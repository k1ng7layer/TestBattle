using Game.Factories.States.Battle;
using Game.StateMachine.StateMachine.Impl;

namespace Game.Services.StateMachineInitializer.Battle.Impl
{
    public class BattleStateMachineInitializer : IBattleStateMachineInitializer
    {
        private readonly IBattleStartNewRoundStateFactory _startNewRoundStateFactory;
        private readonly IBattleWaitForUnitsAttackStateFactory _waitForUnitsAttackStateFactory;
        private readonly IBattleCompleteStateFactory _battleCompleteStateFactory;

        public BattleStateMachineInitializer(
            IBattleStartNewRoundStateFactory startNewRoundStateFactory,
            IBattleWaitForUnitsAttackStateFactory waitForUnitsAttackStateFactory,
            IBattleCompleteStateFactory battleCompleteStateFactory
        )
        {
            _startNewRoundStateFactory = startNewRoundStateFactory;
            _waitForUnitsAttackStateFactory = waitForUnitsAttackStateFactory;
            _battleCompleteStateFactory = battleCompleteStateFactory;
            _waitForUnitsAttackStateFactory = waitForUnitsAttackStateFactory;
        }
        
        public void InitializeStates(BattleStateMachine battleStateMachine)
        {
            battleStateMachine.AddState(_startNewRoundStateFactory.Create(battleStateMachine));
            battleStateMachine.AddState(_waitForUnitsAttackStateFactory.Create(battleStateMachine));
            battleStateMachine.AddState(_battleCompleteStateFactory.Create(battleStateMachine));
        }
    }
}