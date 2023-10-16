using System;
using System.Collections.Generic;
using Game.Models.Modifiers;
using Game.Presenters.Unit;
using Game.Settings.Buffs;

namespace Game.Models.Buffs
{
    public class Buff
    {
        private readonly List<AttributeModifier> _staticModifiers = new();
        private readonly List<AttributeModifier> _attackModifiers = new();
        private IUnit _target;
        private int _tick;
        private int _lifeTime;

        public int TickLeft => _lifeTime;

        public Buff(IBuffsSettingsBase buffsSettingsBase, string buffName)
        {
            BuffName = buffName;
            var settings = buffsSettingsBase.Get(buffName);
            _lifeTime = settings.LifeTime;
            
            foreach (var staticAttribute in settings.StaticAttributeModifiers)
            {
                _staticModifiers.Add(new AttributeModifier(
                    staticAttribute.AttributeType, 
                    staticAttribute.ModifierType, 
                    staticAttribute.Value));
            }

            foreach (var attackModifier in settings.AttackAttributeModifiers)
            {
                _attackModifiers.Add(new AttributeModifier(
                    attackModifier.AttributeType, 
                    attackModifier.ModifierType, 
                    attackModifier.Value));
            }
        }
        
        public String BuffName { get; }

        public void Apply(IUnit target)
        {
            _target = target;
            
            foreach (var staticModifier in _staticModifiers)
            {
                if (target.Attributes.TryGetValue(staticModifier.AttributeType, out var attribute))
                {
                    attribute.AddModifier(staticModifier);
                }
            }

            foreach (var attackModifier in _attackModifiers)
            {
                target.AddAttackModifier(attackModifier);
            }
        }

        public void Disable()
        {
            foreach (var staticModifier in _staticModifiers)
            {
                if (_target.Attributes.TryGetValue(staticModifier.AttributeType, out var attribute))
                {
                    attribute.TryRemoveModifier(staticModifier);
                }
            }

            foreach (var attackModifier in _attackModifiers)
            {
                _target.RemoveAttackModifier(attackModifier);
            }

            //_target.TryRemoveBuff(this);
        }

        public void Tick()
        {
            _lifeTime--;
            
            if(_lifeTime == 0)
                Disable();
        }
    }
}