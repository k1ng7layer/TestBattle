using Game.StateMachine.StateMachine;

namespace Game.StateMachine.States
{
    public abstract class StateBase
    {
        public StateBase(StateMachineBase stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        protected StateMachineBase StateMachine { get; }
        
        public abstract EBattleState StateName { get; }

        public void Execute()
        {
            OnExecute();
        }

        protected virtual void OnExecute()
        { }
    }
}