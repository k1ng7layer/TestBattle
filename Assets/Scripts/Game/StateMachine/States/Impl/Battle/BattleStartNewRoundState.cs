using Game.StateMachine.StateMachine.Impl;

namespace Game.StateMachine.States.Impl.Battle
{
    public class BattleStartNewRoundState : StateBase
    {
        private readonly BattleStateMachine _battleStateMachine;

        public BattleStartNewRoundState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
            _battleStateMachine = battleStateMachine;
        }

        public override EBattleState StateName => EBattleState.StartNewRound;

        protected override void OnExecute()
        {
            _battleStateMachine.LeftPlayerSm.ChangeState(EBattleState.WaitForAction);
            _battleStateMachine.RightPlayerSm.ChangeState(EBattleState.WaitForTurn);
            
            _battleStateMachine.ChangeState(EBattleState.WaitForPlayersAttack);
        }
    }
}