using System;
using Game.StateMachine.States;

namespace Game.StateMachine.StateMachine
{
    public interface IStateProvider
    {
        StateBase CurrentStateBase { get; }
        Action<EBattleState> StateChanged { get; }
    }
}