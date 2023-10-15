using System.Collections.Generic;
using Game.Models.Combat;

namespace Game.Services.TargetService.Impl
{
    public class СircularTargetService : ITargetService
    {
        private BattleMember _leftMember;
        private BattleMember _rightMember;
        
        public void AddBattleMembers(IEnumerable<BattleMember> battleMembers)
        {
            foreach (var battleMember in battleMembers)
            {
                switch (battleMember.BattleTeam)
                {
                    case EBattleTeam.Right:
                        _rightMember = battleMember;
                        break;
                    case EBattleTeam.Left:
                        _leftMember = battleMember;
                        break;
                }
            }
        }
        
        public BattleMember GetTarget(BattleMember attacker)
        {
            BattleMember target = null;
            
            switch (attacker.BattleTeam)
            {
                case EBattleTeam.Right:
                    target = _leftMember;
                    break;
                case EBattleTeam.Left:
                    target = _rightMember;
                    break;
            }

            return target;
        }
    }
}