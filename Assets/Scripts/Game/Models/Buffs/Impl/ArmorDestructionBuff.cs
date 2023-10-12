using System.Collections.Generic;
using Game.Models.Attributes;
using Game.Models.Modifiers;

namespace Game.Models.Buffs.Impl
{
    public class ArmorDestructionBuff : BuffBase
    {
        private readonly AttributeModifier _attributeModifier = new(EAttributeType.Armor, EModifierType.Substract, -10);
        protected readonly int _lifeTime;

        
        
        public override EBuffType BuffType => EBuffType.ArmorDestruction;
        public override int LifeTime => _lifeTime;
        
        protected override void OnApply(List<AttributeModifier> attributeModifiers)
        {
            Target.AddAttackModifier(_attributeModifier);
        }
        
        protected override void OnDisable()
        {
            Target.RemoveAttackModifier(_attributeModifier);
        }

      
    }
}