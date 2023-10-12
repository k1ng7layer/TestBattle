using System.Collections.Generic;
using Game.Models.Attributes;
using Game.Models.Modifiers;

namespace Game.Models.Buffs.Impl
{
    public class ArmorSelfBuff : BuffBase
    {
        public override EBuffType BuffType => EBuffType.ArmorSelf;
        public override int LifeTime => 1;
        
        protected override void OnApply(List<AttributeModifier> attributeModifiers)
        {
            attributeModifiers.Add(new AttributeModifier(EAttributeType.Armor, EModifierType.Add, 50f));
        }
    }
}