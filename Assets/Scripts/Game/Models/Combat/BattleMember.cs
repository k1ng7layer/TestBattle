using Game.Presenters.Unit;

namespace Game.Models.Combat
{
    public class BattleMember
    {
        public BattleMember(
            IUnit unit, 
            EBattleTeam battleTeam, 
            int battleOrder
        )
        {
            Unit = unit;
            BattleTeam = battleTeam;
            BattleOrder = battleOrder;
        }

        public IUnit Unit { get; }
        public EBattleTeam BattleTeam { get; }
        public int BattleOrder { get; }
    }
}