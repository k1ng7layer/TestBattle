using System;
using Game.Presenters.Unit;

namespace Game.Services.AttackManager
{
    public interface IAttackManager
    {
        event Action AttackPerformed;
        void DoAttack(IUnit attacker, IUnit target);
        void ApplyBuff(IUnit attacker);
    }
}