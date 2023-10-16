using System;
using System.Collections.Generic;
using Game.Models.Attributes;
using Game.Models.Buffs;
using Game.Models.Modifiers;
using Game.Views.Unit;
using UnityEngine;

namespace Game.Presenters.Unit.Impl
{
    public class Unit : IUnit
    {
        private readonly IUnitView _unitView;
        private readonly CharacterAttribute _health;
        private readonly CharacterAttribute _armor;
        private readonly CharacterAttribute _vampirism;
        private readonly Dictionary<EBuffType, Buff> _buffs = new();
        private readonly List<AttributeModifier> _attackModifiers = new();
        private readonly Dictionary<EAttributeType, CharacterAttribute> _characterAttributes = new();

        public Unit(IUnitView unitView)
        {
            _unitView = unitView;
            _characterAttributes.Add(EAttributeType.Armor, new CharacterAttribute(60, 0, 100));
            _characterAttributes.Add(EAttributeType.Health, new CharacterAttribute(100, 0, 100));
            _characterAttributes.Add(EAttributeType.Vampirism, new CharacterAttribute(20, 0, 100));
            _characterAttributes.Add(EAttributeType.AttackDamage, new CharacterAttribute(20, 0, 100));
        }

        public event Action<Buff> BuffExpired;
        public IReadOnlyDictionary<EAttributeType, CharacterAttribute> Attributes => _characterAttributes;
        public IReadOnlyDictionary<EBuffType, Buff> StaticBuffs => _buffs;
        public IEnumerable<AttributeModifier> AttackModifiers => _attackModifiers;
        
        public event Action Dead;
        public event Action<Buff> Buffed;

        public void AddBuff(Buff buff)
        {
            var hasBuff = _buffs.ContainsKey(buff.BuffType);
            
            if(hasBuff)
                return;
            
            _buffs.Add(buff.BuffType, buff);
            Debug.Log($"Added buff {buff.BuffType}");
            
            buff.Apply(this);
            
            Buffed?.Invoke(buff);
        }

        public bool TryRemoveBuff(Buff buff)
        {
            var result =  _buffs.Remove(buff.BuffType);

            if (result)
            {
                BuffExpired?.Invoke(buff);
            }

            return result;
        }
        
        public void TakeDamage(float attackDamage, IEnumerable<AttributeModifier> attributeModifiers)
        {
            var health = _characterAttributes[EAttributeType.Health];
            var armor = _characterAttributes[EAttributeType.Armor];
            var vampirism = _characterAttributes[EAttributeType.Vampirism];
            var attackAttribute = _characterAttributes[EAttributeType.AttackDamage];
            
            foreach (var attributeModifier in attributeModifiers)
            {
                switch (attributeModifier.AttributeType)
                {
                    case EAttributeType.Health:
                        health.Value -= attributeModifier.Value;
                        break;
                    case EAttributeType.Armor:
                        armor.Value -= attributeModifier.Value;
                        break;
                    case EAttributeType.Vampirism:
                        vampirism.Value -= attributeModifier.Value;
                        break;
                    case EAttributeType.AttackDamage:
                        attackAttribute.Value -= attributeModifier.Value;
                        break;
                }
            }
            
            var armorAffection = attackDamage - attackDamage / 100f * armor.Value;
            health.Value -= armorAffection;
            
            _unitView.OnTakeDamage();
            
            if(health.Value <= 0)
                Dead?.Invoke();
        }

        public void PerformAttack()
        {
            var vampirism = _characterAttributes[EAttributeType.Vampirism];
            var attackDamage = _characterAttributes[EAttributeType.AttackDamage];
            var health = _characterAttributes[EAttributeType.Health];

            var selfHealing = (attackDamage.Value / 100f) * vampirism.Value;
            Debug.Log($"selfHealing = {selfHealing}");
            
            health.Value += selfHealing;
        }

        public void AddAttackModifier(AttributeModifier attributeModifier)
        {
            _attackModifiers.Add(attributeModifier);
        }

        public bool RemoveAttackModifier(AttributeModifier attributeModifier)
        {
           return _attackModifiers.Remove(attributeModifier);
        }

        public void UpdateState()
        {
            var expired = new List<Buff>();
            
            foreach (var buff in _buffs.Values)
            {
                buff.Tick();
                
                if(buff.TickLeft == 0)
                    expired.Add(buff);
            }

            foreach (var buff in expired)
            {
                TryRemoveBuff(buff);
            }
        }
    }
}