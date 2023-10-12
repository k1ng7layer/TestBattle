using System.Collections.Generic;
using Game.Models.Attributes;
using Game.Models.Modifiers;

namespace Game.Models.Buffs.Impl
{
    public class DoubleDamageBuff : BuffBase
    {
        public override EBuffType BuffType => EBuffType.DoubleDamage;
        public override int LifeTime => 5;
        
        protected override void OnApply(List<AttributeModifier> attributeModifiers)
        {
            attributeModifiers.Add(new AttributeModifier(EAttributeType.AttackDamage, EModifierType.Multiply, 2));
        }
        
    }
}