using Game.Models.Attributes;

namespace Game.Models.Modifiers
{
    public class AttributeModifier
    {
        public readonly EAttributeType AttributeType;
        public readonly EModifierType ModifierType;
        public readonly float Value;

        public AttributeModifier(
            EAttributeType attributeType, 
            EModifierType modifierType, 
            float value
        )
        {
            AttributeType = attributeType;
            ModifierType = modifierType;
            Value = value;
        }
    }
}