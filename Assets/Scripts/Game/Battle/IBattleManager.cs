using System;
using System.Collections.Generic;
using Game.Models.Combat;

namespace Game.Battle
{
    public interface ICombatManager
    {
        event Action<BattleMember, BattleMember> BattleStarted;
        void StartCombat(List<BattleMember> battleMembers);
        void Attack();
        void ApplyBuff();
    }
}