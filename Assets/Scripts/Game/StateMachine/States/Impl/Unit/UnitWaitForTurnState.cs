using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;

namespace Game.StateMachine.States.Impl.Unit
{
    public class UnitWaitForTurnState : UnitStateBase
    {
        public UnitWaitForTurnState(IUnit unit, UnitStateMachine stateMachine) : base(unit, stateMachine)
        {
        }

        public override EBattleState StateName => EBattleState.WaitForTurn;
    }
}