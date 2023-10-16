using Game.Models.Attributes;
using Game.Presenters.Unit;
using Game.Services.Buffs;
using Game.Settings.Battle;

namespace Game.Services.AttackManager.Impl
{
    public class AttackManager : IAttackManager
    {
        private readonly IBuffProvider _buffProvider;
        private readonly IBattleSettingsBase _battleSettingsBase;

        public AttackManager(
            IBuffProvider buffProvider, 
            IBattleSettingsBase battleSettingsBase
        )
        {
            _buffProvider = buffProvider;
            _battleSettingsBase = battleSettingsBase;
        }
        
        public void DoAttack(IUnit attacker, IUnit target)
        {
            var damage = attacker.Attributes[EAttributeType.AttackDamage];
            
            var totalDamage = CalculateDamage(target, damage.Value);
            attacker.PerformAttack();

            target.TakeDamage(totalDamage, attacker.AttackModifiers);
        }

        public void ApplyBuff(IUnit attacker)
        {
            var hasMaxBuffs = attacker.StaticBuffs.Count == _battleSettingsBase.MaxActiveBuffs;

            if (!hasMaxBuffs)
            {
                var getBuff = _buffProvider.TryGetBuff(attacker.StaticBuffs);
                attacker.AddBuff(getBuff);
            }
        }

        private float CalculateDamage(IUnit target, float damage)
        {
            var armor = target.Attributes[EAttributeType.Armor];
            var armorAffected = damage - damage / 100f * armor.Value;

            return armorAffected;
        }
    }
}