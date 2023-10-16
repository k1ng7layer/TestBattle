using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;

namespace Game.StateMachine.States.Impl.Unit
{
    public class UnitWaitForAttackStateBase : UnitStateBase
    {
        public UnitWaitForAttackStateBase(
            IUnit unit,
            UnitStateMachine unitStateMachine
        ) : base(unit, unitStateMachine)
        {
            
        }

        public override EBattleState StateName => EBattleState.WaitForAttack;
    }
}