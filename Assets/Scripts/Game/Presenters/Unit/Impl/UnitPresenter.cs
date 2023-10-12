using System;
using System.Collections.Generic;
using Game.Models.Attributes;
using Game.Models.Buffs;
using Game.Models.Modifiers;
using Game.Views.Unit;

namespace Game.Presenters.Unit.Impl
{
    public class Unit : IUnit
    {
        private readonly IUnitView _unitView;
        private readonly CharacterAttribute _health;
        private readonly CharacterAttribute _armor;
        private readonly CharacterAttribute _vampirism;
        private readonly Dictionary<EBuffType, BuffBase> _buffs = new();
        private readonly List<AttributeModifier> _attackModifiers = new();

        public Unit(IUnitView unitView)
        {
            _unitView = unitView;
        }

        public CharacterAttribute Health => _health;
        public CharacterAttribute Armor => _armor;
        public CharacterAttribute Vampirism => _vampirism;
        public CharacterAttribute AttackDamage => _vampirism;
        public IReadOnlyDictionary<EAttributeType, CharacterAttribute> Attributes { get; }
        public IReadOnlyDictionary<EBuffType, BuffBase> StaticBuffs => _buffs;
        
        public IEnumerable<AttributeModifier> AttackModifiers => _attackModifiers;
        
        public event Action Dead;
        
        public void AddBuff(BuffBase buff)
        {
            var hasBuff = _buffs.ContainsKey(buff.BuffType);
            
            if(hasBuff)
                return;
            
            _buffs.Add(buff.BuffType, buff);
            
            buff.Apply(this);
        }

        public bool TryRemoveBuff(BuffBase buff)
        {
            return _buffs.Remove(buff.BuffType);
        }
        
        public void HandleAttack(float attackDamage, List<AttributeModifier> attributeModifiers)
        {
            var armorAffection = attackDamage - attackDamage / 100f * Armor.Value;
            Health.Value -= armorAffection;
            
            foreach (var attributeModifier in attributeModifiers)
            {
                switch (attributeModifier.AttributeType)
                {
                    case EAttributeType.Health:
                        Health.Value -= attributeModifier.Value;
                        break;
                    case EAttributeType.Armor:
                        Armor.Value -= attributeModifier.Value;
                        break;
                    case EAttributeType.Vampirism:
                        Vampirism.Value -= attributeModifier.Value;
                        break;
                    case EAttributeType.AttackDamage:
                        AttackDamage.Value -= attributeModifier.Value;
                        break;
                }
            }
            
            _unitView.OnTakeDamage();
            
            if(Health.Value <= 0)
                Dead?.Invoke();
        }

        public void AddAttackModifier(AttributeModifier attributeModifier)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAttackModifier(AttributeModifier attributeModifier)
        {
            throw new NotImplementedException();
        }
    }
}