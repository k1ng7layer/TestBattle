using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;

namespace Game.Services.StateMachineInitializer.Unit
{
    public interface IUnitStateMachineInitializer
    {
        void InitializeStates(IUnit selfUnit, IUnit enemyUnit, UnitStateMachine stateMachine);
    }
}