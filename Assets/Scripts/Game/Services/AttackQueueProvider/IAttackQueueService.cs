using System;
using System.Collections.Generic;
using Game.Models.Combat;

namespace Game.Services.AttackQueueProvider
{
    public interface IAttackQueueService
    {
        BattleMember ActiveMember { get; }
        int AttackersInRoundLeft { get; }
        event Action<BattleMember> ActiveUnitChanged; 
        void InitializeQueue(List<BattleMember> battleMembers);
        BattleMember NextAttacker();
    }
}