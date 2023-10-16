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

            //можно вынести в отдельный стейт
            
            if (leftPlayerState == EBattleState.WaitForRoundEnd && 
                rightPlayerState == EBattleState.WaitForRoundEnd)
            {
                StartNewRound();
            }

            if (leftPlayerState == EBattleState.WaitForRoundEnd &&
                rightPlayerState == EBattleState.WaitForTurn)
            {
                _battleStateMachine.RightPlayerSm.ChangeState(EBattleState.WaitForAction);
            }
                
        }

        private void StartNewRound()
        {
            var leftPlayerSM = _battleStateMachine.LeftPlayerSm;
            var rightPlayerSM = _battleStateMachine.RightPlayerSm;
            
            leftPlayerSM.Unit.UpdateState();
            rightPlayerSM.Unit.UpdateState();
                
            StateMachine.ChangeState(EBattleState.StartNewRound);
        }
    }
}