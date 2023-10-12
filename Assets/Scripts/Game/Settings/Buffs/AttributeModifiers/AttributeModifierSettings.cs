using Game.Models.Attributes;
using Game.Models.Modifiers;
using UnityEngine;

namespace Game.Settings.Buffs.AttributeModifiers
{
    [CreateAssetMenu(menuName = "Settings/Buffs/" + nameof(AttributeModifierSettings), fileName = nameof(AttributeModifierSettings))]
    public class AttributeModifierSettings : ScriptableObject
    {
        [SerializeField] private EAttributeType attributeType;
        [SerializeField] private EModifierType modifierType;
        [SerializeField] private float value;

        public EAttributeType AttributeType => attributeType;
        public EModifierType ModifierType => modifierType;
        public float Value => value;
    }
}