using System;
using System.Collections.Generic;
using Game.Models.Attributes;
using Game.Models.Buffs;
using Game.Models.Modifiers;
using Game.Settings.Unit;
using Game.Views.Unit;

namespace Game.Presenters.Unit.Impl
{
    public class Unit : IUnit
    {
        private readonly IUnitView _unitView;
        private readonly Dictionary<string, Buff> _buffs = new();
        private readonly List<AttributeModifier> _attackModifiers = new();
        private readonly Dictionary<EAttributeType, CharacterAttribute> _characterAttributes = new();

        public Unit(IUnitView unitView, UnitParameters unitParameters)
        {
            _unitView = unitView;

            Init(unitParameters);
        }

        public IReadOnlyDictionary<EAttributeType, CharacterAttribute> Attributes => _characterAttributes;
        public IReadOnlyDictionary<string, Buff> StaticBuffs => _buffs;
        public IEnumerable<AttributeModifier> AttackModifiers => _attackModifiers;
        
        public event Action<Buff> BuffExpired;
        public event Action Dead;
        public event Action<Buff> Buffed;
        
        private void Init(UnitParameters parameters)
        {
            _characterAttributes.Add(EAttributeType.Armor, 
                new CharacterAttribute(
                    parameters.StartArmor, 
                    0, 
                    parameters.MaxArmor));
            
            _characterAttributes.Add(EAttributeType.Health, 
                new CharacterAttribute(
                    parameters.StartHealth, 
                    0, 
                    parameters.MaxHealth));
            
            _characterAttributes.Add(EAttributeType.Vampirism, 
                new CharacterAttribute(
                    parameters.StartVampirism, 
                    0, 
                    parameters.MaxVampyrism));
            
            _characterAttributes.Add(EAttributeType.AttackDamage, 
                new CharacterAttribute(
                    parameters.StartAttackDamage, 
                    0, 
                    parameters.MaxAttackDamage));
        }

        public void AddBuff(Buff buff)
        {
            var hasBuff = _buffs.ContainsKey(buff.BuffName);
            
            if(hasBuff)
                return;
            
            _buffs.Add(buff.BuffName, buff);
            
            buff.Apply(this);
            
            Buffed?.Invoke(buff);
        }
        
        public void TakeDamage(float damage, IEnumerable<AttributeModifier> attributeModifiers)
        {
            var health = _characterAttributes[EAttributeType.Health];
            
            foreach (var attributeModifier in attributeModifiers)
            {
                var characterAttribute = _characterAttributes[attributeModifier.AttributeType];
                characterAttribute.Value -= attributeModifier.Value;
            }
            
            health.Value -= damage;
            
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
        
        private bool TryRemoveBuff(Buff buff)
        {
            var result =  _buffs.Remove(buff.BuffName);

            if (result)
            {
                BuffExpired?.Invoke(buff);
            }

            return result;
        }
    }
}