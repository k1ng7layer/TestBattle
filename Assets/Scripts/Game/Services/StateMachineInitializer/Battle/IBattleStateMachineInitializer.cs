using Game.StateMachine.StateMachine.Impl;

namespace Game.Services.StateMachineInitializer.Battle
{
    public interface IBattleStateMachineInitializer
    {
        void InitializeStates(BattleStateMachine battleStateMachine);
    }
}