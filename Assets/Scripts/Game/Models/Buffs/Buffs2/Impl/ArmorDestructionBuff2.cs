using System.Collections.Generic;
using Game.Models.Modifiers;
using Game.Settings.Buffs;

namespace Game.Models.Buffs.Buffs2.Impl
{
    public class ArmorDestructionBuff2 : AttackBuff
    {
        private readonly IBuffsSettingsBase _buffsSettingsBase;

        public ArmorDestructionBuff2(IBuffsSettingsBase buffsSettingsBase)
        {
            _buffsSettingsBase = buffsSettingsBase;
        }
        
        protected override EBuffType BuffType => EBuffType.ArmorDestruction;

        protected override void SetupModifiers(List<AttributeModifier> attributeModifiers)
        {
            var settings = _buffsSettingsBase.Get(BuffType);

            foreach (var attributeModifier in settings.AttributeModifiers)
            {
                attributeModifiers.Add(new AttributeModifier(
                    attributeModifier.AttributeType, 
                    attributeModifier.ModifierType, 
                    attributeModifier.Value));
            }
        }
    }
}