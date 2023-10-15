using System.Collections.Generic;
using Game.Models.Buffs;
using Game.Settings.Buffs.AttributeModifiers;
using UnityEngine;

namespace Game.Settings.Buffs
{ 
    [CreateAssetMenu(menuName = "Settings/Buffs/" + nameof(BuffSettings), fileName = nameof(BuffSettings))]
    public class BuffSettings : ScriptableObject
    {
        [SerializeField] private EBuffType buffType;
        [SerializeField] private int lifeTime;
        [SerializeField] private List<AttributeModifierSettings> staticAttributeModifiers;
        [SerializeField] private List<AttributeModifierSettings> attackAttributeModifiers;

        public EBuffType BuffType => buffType;
        public int LifeTime => lifeTime;
        public IEnumerable<AttributeModifierSettings> StaticAttributeModifiers => staticAttributeModifiers;
        public IEnumerable<AttributeModifierSettings> AttackAttributeModifiers => attackAttributeModifiers;
    }
}