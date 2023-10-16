using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;

namespace Game.StateMachine.States.Impl.Unit
{
    public class UnitStartNewRoundStateBase : UnitStateBase
    {
        public UnitStartNewRoundStateBase(
            IUnit unit,
            UnitStateMachine stateMachine
        ) : base(unit, stateMachine)
        {

        }

        public override EBattleState StateName => EBattleState.StartNewRound;

        protected override void OnExecute()
        {
            Unit.UpdateState();
            
            StateMachine.ChangeState(EBattleState.WaitForAction);
        }
    }
}