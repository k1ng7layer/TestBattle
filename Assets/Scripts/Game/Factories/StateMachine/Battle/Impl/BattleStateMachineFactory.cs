using Game.StateMachine.StateMachine.Impl;
using Zenject;

namespace Game.Factories.StateMachine.Battle.Impl
{
    public class BattleStateMachineFactory : PlaceholderFactory<UnitStateMachine, UnitStateMachine, BattleStateMachine>, 
        IBattleStateMachineFactory
    {
        
    }
}