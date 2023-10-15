using System;
using Game.Models.Attributes;
using Game.Presenters.Unit;
using Game.Services.Buffs;

namespace Game.Services.AttackManager.Impl
{
    public class AttackManager : IAttackManager
    {
        private readonly IBuffProvider _buffProvider;

        public AttackManager(IBuffProvider buffProvider)
        {
            _buffProvider = buffProvider;
        }

        public event Action AttackPerformed;

        public void DoAttack(IUnit attacker, IUnit target)
        {
            var damage = attacker.Attributes[EAttributeType.AttackDamage];
            
            attacker.PerformAttack();
            
            target.TakeDamage(damage.Value, attacker.AttackModifiers);
            
            AttackPerformed?.Invoke();
        }

        public void ApplyBuff(IUnit attacker)
        {
            var hasMaxBuffs = attacker.StaticBuffs.Count == 2;

            if (!hasMaxBuffs)
            {
                var getBuff = _buffProvider.TryGetBuff(attacker.StaticBuffs);
                attacker.AddBuff(getBuff);
            }
        }
    }
}