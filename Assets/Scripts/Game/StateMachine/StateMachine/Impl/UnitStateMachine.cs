using Game.Presenters.Unit;

namespace Game.StateMachine.StateMachine.Impl
{
    public class UnitStateMachine : StateMachineBase
    {

        public UnitStateMachine(IUnit unit)
        {
            Unit = unit;
        }
        
        public IUnit Unit { get; }
    }
}