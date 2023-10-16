using System;
using System.Collections.Generic;
using Game.StateMachine.States;

namespace Game.StateMachine.StateMachine
{
    public abstract class StateMachineBase
    {
        private readonly Dictionary<EBattleState, StateBase> _states = new();
        private StateBase _currentStateBase;

        public StateBase CurrentStateBase => _currentStateBase;

        public event Action<EBattleState> StateChanged; 

        public void AddState(StateBase stateBase)
        {
            _states.Add(stateBase.StateName, stateBase);
        }

        public void ChangeState(EBattleState state)
        {
            _currentStateBase = _states[state];
            
            StateChanged?.Invoke(state);
            
            _currentStateBase.Execute();
            
        }
    }
}