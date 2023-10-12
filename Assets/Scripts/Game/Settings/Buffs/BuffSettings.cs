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
        [SerializeField] private List<AttributeModifierSettings> attributeModifiers;
        [SerializeField] private int lifeTime;

        public EBuffType BuffType => buffType;
        public IEnumerable<AttributeModifierSettings> AttributeModifiers => attributeModifiers;
        public int LifeTime => lifeTime;
    }
}