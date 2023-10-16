namespace Game.StateMachine.StateMachine.Impl
{
    public class BattleStateMachine : StateMachineBase
    {
        private UnitStateMachine _leftUnitSM;
        private UnitStateMachine _rightUnitSM;
        
        public UnitStateMachine RightPlayerSm => _rightUnitSM;
        public UnitStateMachine LeftPlayerSm => _leftUnitSM;

        //public event Action<BattleMember, BattleMember> BattleStarted;
        
        public void Initialize(UnitStateMachine leftUnitSM, UnitStateMachine rightUnitSM)
        {
            _leftUnitSM = leftUnitSM;
            _rightUnitSM = rightUnitSM;
        }

        public void StartBattle()
        {
            //BattleStarted?.Invoke();
        }
    }
}