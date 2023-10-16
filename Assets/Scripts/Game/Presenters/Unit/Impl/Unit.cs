﻿using System;
using System.Collections.Generic;
using Game.Models.Attributes;
using Game.Models.Buffs;
using Game.Models.Modifiers;
using Game.Settings.Unit;
using Game.Views.Unit;
using UnityEngine;

namespace Game.Presenters.Unit.Impl
{
    public class Unit : IUnit
    {
        private readonly IUnitView _unitView;
        private readonly UnitParameters _unitParameters;
        private readonly CharacterAttribute _health;
        private readonly CharacterAttribute _armor;
        private readonly CharacterAttribute _vampirism;
        private readonly Dictionary<string, Buff> _buffs = new();
        private readonly List<AttributeModifier> _attackModifiers = new();
        private readonly Dictionary<EAttributeType, CharacterAttribute> _characterAttributes = new();

        public Unit(IUnitView unitView, UnitParameters unitParameters)
        {
            _unitView = unitView;
            _unitParameters = unitParameters;

            Init();
        }

        public event Action<Buff> BuffExpired;
        public IReadOnlyDictionary<EAttributeType, CharacterAttribute> Attributes => _characterAttributes;
        public IReadOnlyDictionary<string, Buff> StaticBuffs => _buffs;
        public IEnumerable<AttributeModifier> AttackModifiers => _attackModifiers;
        
        public event Action Dead;
        public event Action<Buff> Buffed;

        public void AddBuff(Buff buff)
        {
            var hasBuff = _buffs.ContainsKey(buff.BuffName);
            
            if(hasBuff)
                return;
            
            _buffs.Add(buff.BuffName, buff);
            Debug.Log($"Added buff {buff.BuffName}");
            
            buff.Apply(this);
            
            Buffed?.Invoke(buff);
        }

        public bool TryRemoveBuff(Buff buff)
        {
            var result =  _buffs.Remove(buff.BuffName);

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

        private void Init()
        {
            _characterAttributes.Add(EAttributeType.Armor, 
                new CharacterAttribute(_unitParameters.StartArmor, 0, _unitParameters.MaxArmor));
            
            _characterAttributes.Add(EAttributeType.Health, 
                new CharacterAttribute(_unitParameters.StartHealth, 0, _unitParameters.MaxHealth));
            
            _characterAttributes.Add(EAttributeType.Vampirism, 
                new CharacterAttribute(_unitParameters.StartVampirism, 0, _unitParameters.MaxVampyrism));
            
            _characterAttributes.Add(EAttributeType.AttackDamage, 
                new CharacterAttribute(_unitParameters.StartAttackDamage, 0, _unitParameters.MaxAttackDamage));
        }
    }
}