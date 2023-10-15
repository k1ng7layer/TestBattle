using Game.Factories.BattleMembers.Impl;
using Game.Models.Combat;
using Game.Presenters.Unit;
using Zenject;

namespace Game.Factories.BattleMembers
{
    public class BattleMemberFactory : PlaceholderFactory<IUnit, EBattleTeam, int, BattleMember>, 
        IBattleMemberFactory
    {
        public override BattleMember Create(IUnit unit, EBattleTeam team, int order)
        {
            var member = base.Create(unit, team, order);
            return member;
        }
    }
}