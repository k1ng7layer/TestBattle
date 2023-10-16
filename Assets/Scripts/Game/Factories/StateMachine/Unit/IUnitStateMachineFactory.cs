using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;
using Zenject;

namespace Game.Factories.StateMachine.Unit
{
    public interface IUnitStateMachineFactory : IFactory<IUnit, UnitStateMachine>
    {
        
    }
}