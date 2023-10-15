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
        event Action<Buff> Buffed;
        event Action<Buff> BuffExpired;
        IReadOnlyDictionary<EAttributeType, CharacterAttribute> Attributes { get; }
        IReadOnlyDictionary<EBuffType, Buff> StaticBuffs { get; }
        IEnumerable<AttributeModifier> AttackModifiers { get; }
        void AddBuff(Buff buff);
        bool TryRemoveBuff(Buff buff);
        void TakeDamage(float attackDamage, IEnumerable<AttributeModifier> attributeModifiers);
        void PerformAttack();
        void AddAttackModifier(AttributeModifier attributeModifier);
        bool RemoveAttackModifier(AttributeModifier attributeModifier);
        void UpdateState();
    }
}