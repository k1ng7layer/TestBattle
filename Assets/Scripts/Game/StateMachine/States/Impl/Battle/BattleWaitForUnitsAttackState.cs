using Game.StateMachine.StateMachine.Impl;

namespace Game.StateMachine.States.Impl.Battle
{
    public class BattleWaitForUnitsAttackState : StateBase
    {
        private readonly BattleStateMachine _battleStateMachine;

        public BattleWaitForUnitsAttackState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
            _battleStateMachine = battleStateMachine;
        }

        public override EBattleState StateName => EBattleState.WaitForPlayersAttack;

        protected override void OnExecute()
        {
            _battleStateMachine.LeftPlayerSm.StateChanged += UnitsStateMachineOnStateChanged;
            _battleStateMachine.RightPlayerSm.StateChanged += UnitsStateMachineOnStateChanged;
        }

        private void UnitsStateMachineOnStateChanged(EBattleState _)
        {
            var leftPlayerSM = _battleStateMachine.LeftPlayerSm;
            var rightPlayerSM = _battleStateMachine.RightPlayerSm;
            
            var leftPlayerState = leftPlayerSM.CurrentStateBase.StateName;
            var rightPlayerState = rightPlayerSM.CurrentStateBase.StateName;

            if (leftPlayerState == EBattleState.WaitForRoundEnd && rightPlayerState == EBattleState.WaitForRoundEnd)
            {
                leftPlayerSM.Unit.UpdateState();
                rightPlayerSM.Unit.UpdateState();
                
                StateMachine.ChangeState(EBattleState.StartNewRound);
            }
            
            if(leftPlayerState == EBattleState.WaitForRoundEnd && rightPlayerState == EBattleState.WaitForTurn)
                _battleStateMachine.RightPlayerSm.ChangeState(EBattleState.WaitForAction);
        }
    }
}