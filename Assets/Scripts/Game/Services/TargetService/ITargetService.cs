using System.Collections.Generic;
using Game.Models.Combat;

namespace Game.Services.TargetService
{
    public interface ITargetService
    {
        void AddBattleMembers(IEnumerable<BattleMember> battleMembers);
        BattleMember GetTarget(BattleMember attacker);
    }
}