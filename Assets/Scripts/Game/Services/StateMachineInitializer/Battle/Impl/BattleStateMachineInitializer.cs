using Game.Factories.States.Battle;
using Game.StateMachine.StateMachine.Impl;

namespace Game.Services.StateMachineInitializer.Battle.Impl
{
    public class BattleStateMachineInitializer : IBattleStateMachineInitializer
    {
        private readonly IBattleStartNewRoundStateFactory _startNewRoundStateFactory;
        private readonly IBattleWaitForUnitsAttackStateFactory _waitForUnitsAttackStateFactory;

        public BattleStateMachineInitializer(
            IBattleStartNewRoundStateFactory startNewRoundStateFactory,
            IBattleWaitForUnitsAttackStateFactory waitForUnitsAttackStateFactory
        )
        {
            _startNewRoundStateFactory = startNewRoundStateFactory;
            _waitForUnitsAttackStateFactory = waitForUnitsAttackStateFactory;
            _waitForUnitsAttackStateFactory = waitForUnitsAttackStateFactory;
        }
        
        public void InitializeStates(BattleStateMachine battleStateMachine)
        {
            battleStateMachine.AddState(_startNewRoundStateFactory.Create(battleStateMachine));
            battleStateMachine.AddState(_waitForUnitsAttackStateFactory.Create(battleStateMachine));
        }
    }
}