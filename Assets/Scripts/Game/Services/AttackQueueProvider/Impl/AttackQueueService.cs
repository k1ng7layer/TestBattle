using System;
using System.Collections.Generic;
using Game.Models.Combat;
using Game.Settings.Battle;

namespace Game.Services.AttackQueueProvider.Impl
{
    public class AttackQueueService : IAttackQueueService
    {
        private readonly IBattleSettingsBase _battleSettingsBase;
        private readonly Queue<BattleMember> attackQueue = new();
        
        private List<BattleMember> _battleMembers;
        private BattleMember _activeMember;
        
        public BattleMember ActiveMember => _activeMember;
        public int AttackersInRoundLeft => attackQueue.Count;
        
        public event Action<BattleMember> ActiveUnitChanged;
        
        public void InitializeQueue(List<BattleMember> battleMembers)
        {
            attackQueue.Clear();
            
            _battleMembers = battleMembers;

            FillAttackQueue();

            // _activeMember = attackQueue.Peek();
            // ActiveUnitChanged?.Invoke(_activeMember);
        }

        public BattleMember NextAttacker()
        {
            _activeMember = attackQueue.Dequeue();
            
            ActiveUnitChanged?.Invoke(_activeMember);
            
            return _activeMember;
        }

        private void FillAttackQueue()
        {
            _battleMembers.Sort(BattleMemberComparer);

            foreach (var battleMember in _battleMembers)
            {
                attackQueue.Enqueue(battleMember);
            }
        }
        
        private int BattleMemberComparer(BattleMember a, BattleMember b)
        {
            if (a.BattleOrder > b.BattleOrder)
                return 1;

            if(a.BattleOrder == b.BattleOrder)
                return 0;

            return -1;
        }
    }
}