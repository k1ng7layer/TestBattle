using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;

namespace Game.StateMachine.States
{
    public abstract class UnitStateBase : StateBase
    {
        protected UnitStateBase(IUnit unit, UnitStateMachine stateMachine) : base(stateMachine)
        {
            Unit = unit;
        }
        
        protected IUnit Unit { get; }
    }
}