using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;

namespace Game.Models.Combat
{
    public class BattleMember
    {
        private readonly UnitStateMachine _stateMachine;

        public BattleMember(
            IUnit unit, 
            EBattleTeam battleTeam, 
            int battleOrder,
            UnitStateMachine stateMachine
        )
        {
            _stateMachine = stateMachine;
            Unit = unit;
            BattleTeam = battleTeam;
            BattleOrder = battleOrder;
            StateMachine = stateMachine;
        }

        public IUnit Unit { get; }
        public EBattleTeam BattleTeam { get; }
        public int BattleOrder { get; }
        public UnitStateMachine StateMachine { get;}
    }
}