using Game.Models.Combat;
using Game.Presenters.Unit;
using Zenject;

namespace Game.Factories.BattleMembers.Impl
{
    public interface IBattleMemberFactory : IFactory<IUnit, EBattleTeam, int, BattleMember>
    {
        
    }
}