using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;
using Zenject;

namespace Game.Factories.StateMachine.Unit.Impl
{
    public class UnitStateMachineFactory : PlaceholderFactory<IUnit, UnitStateMachine>, 
        IUnitStateMachineFactory
    {
        
    }
}