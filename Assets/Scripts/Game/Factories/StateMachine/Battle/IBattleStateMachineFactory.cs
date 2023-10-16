using Game.StateMachine.StateMachine.Impl;
using Zenject;

namespace Game.Factories.StateMachine.Battle
{
    public interface IBattleStateMachineFactory : IFactory<UnitStateMachine, UnitStateMachine, BattleStateMachine>
    {
        
    }
}