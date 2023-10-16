using System;
using System.Collections.Generic;
using Game.Models.Attributes;
using Game.Models.Modifiers;
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

        public event Action AttackPerformed;

        public void DoAttack(IUnit attacker, IUnit target)
        {
            var damage = attacker.Attributes[EAttributeType.AttackDamage];
            
            attacker.PerformAttack();
            
            target.TakeDamage(damage.Value, attacker.AttackModifiers);
            
            //CalculateDamage(target, damage.Value, attacker.AttackModifiers);
            
            AttackPerformed?.Invoke();
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

        private void CalculateDamage(IUnit target, float damage, IEnumerable<AttributeModifier> attributeModifiers)
        {
            var health = target.Attributes[EAttributeType.Health];
            var armor = target.Attributes[EAttributeType.Armor];
            var vampirism = target.Attributes[EAttributeType.Vampirism];
            var attackAttribute = target.Attributes[EAttributeType.AttackDamage];
            
            foreach (var attributeModifier in attributeModifiers)
            {
                switch (attributeModifier.AttributeType)
                {
                    case EAttributeType.Health:
                        health.Value -= attributeModifier.Value;
                        break;
                    case EAttributeType.Armor:
                        armor.Value -= attributeModifier.Value;
                        break;
                    case EAttributeType.Vampirism:
                        vampirism.Value -= attributeModifier.Value;
                        break;
                    case EAttributeType.AttackDamage:
                        attackAttribute.Value -= attributeModifier.Value;
                        break;
                }
            }
            
            var armorAffection = damage - damage / 100f * armor.Value;
            health.Value -= armorAffection;
        }
    }
}