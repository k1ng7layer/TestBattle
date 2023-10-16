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

        public Unit(IUnitView unitView, List<UnitAttributeParameters> unitAttributeParameters)
        {
            _unitView = unitView;

            Init(unitAttributeParameters);
        }

        public IReadOnlyDictionary<EAttributeType, CharacterAttribute> Attributes => _characterAttributes;
        public IReadOnlyDictionary<string, Buff> StaticBuffs => _buffs;
        public IEnumerable<AttributeModifier> AttackModifiers => _attackModifiers;
        
        public event Action<Buff> BuffExpired;
        public event Action Dead;
        public event Action<Buff> Buffed;
        
        private void Init(List<UnitAttributeParameters> attributeParameters)
        {
            foreach (var parameter in attributeParameters)
            {
                _characterAttributes.Add(parameter.AttributeType, 
                    new CharacterAttribute(
                        parameter.InitialValue, 
                        parameter.MinValue, 
                        parameter.MaxValue));
            }
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