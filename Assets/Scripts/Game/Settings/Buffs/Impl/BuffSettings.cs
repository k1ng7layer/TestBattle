using System.Collections.Generic;
using Game.Settings.Buffs.AttributeModifiers;
using UnityEngine;

namespace Game.Settings.Buffs.Impl
{ 
    [CreateAssetMenu(menuName = "Settings/Buffs/" + nameof(BuffSettings), fileName = nameof(BuffSettings))]
    public class BuffSettings : ScriptableObject
    {
        [SerializeField] private string buffName;
        [SerializeField] private int lifeTime;
        [SerializeField] private List<AttributeModifierSettings> staticAttributeModifiers;
        [SerializeField] private List<AttributeModifierSettings> attackAttributeModifiers;

        public string BuffName => buffName;
        public int LifeTime => lifeTime;
        public IEnumerable<AttributeModifierSettings> StaticAttributeModifiers => staticAttributeModifiers;
        public IEnumerable<AttributeModifierSettings> AttackAttributeModifiers => attackAttributeModifiers;
    }
}