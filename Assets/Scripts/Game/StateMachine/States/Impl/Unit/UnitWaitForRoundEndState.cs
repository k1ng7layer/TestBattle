using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;

namespace Game.StateMachine.States.Impl.Unit
{
    public class UnitWaitForRoundEndState : UnitStateBase
    {
        
        public UnitWaitForRoundEndState(
            UnitStateMachine stateMachineBase, 
            IUnit unit
        ) : base(unit, stateMachineBase)
        {
            
        }

        public override EBattleState StateName => EBattleState.WaitForRoundEnd;
    }
}