using System;
using System.Collections.Generic;
using Game.Models.Attributes;
using Game.Models.Buffs;
using Game.Models.Modifiers;

namespace Game.Presenters.Unit
{
    public interface IUnit
    {
        event Action Dead;
        
        IReadOnlyDictionary<EAttributeType, CharacterAttribute> Attributes { get; }
        IReadOnlyDictionary<EBuffType, BuffBase> StaticBuffs { get; }
        IEnumerable<AttributeModifier> AttackModifiers { get; }
        void AddBuff(BuffBase buffBase);
        bool TryRemoveBuff(BuffBase buffBase);
        void HandleAttack(float attackDamage, IEnumerable<AttributeModifier> attributeModifiers);
        void AddAttackModifier(AttributeModifier attributeModifier);
        bool RemoveAttackModifier(AttributeModifier attributeModifier);
    }
}