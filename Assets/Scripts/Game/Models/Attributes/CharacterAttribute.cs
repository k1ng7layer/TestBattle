using System.Collections.Generic;
using Game.Models.Modifiers;
using UnityEngine;

namespace Game.Models.Attributes
{
    public class CharacterAttribute
    {
        private readonly HashSet<AttributeModifier> _modifiers = new();
        private float _baseValue;
        private float _additionalValue;
        private bool _isDirty;
        
        public CharacterAttribute(float minValue, float maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }
        
        public float MaxValue { get; }
        public float MinValue { get; }

        public float Value
        {
            get
            {
                if (_isDirty)
                {
                    _additionalValue = CalculateAdditionalValue();
                    _isDirty = false;
                }

                return _baseValue + _additionalValue;
            }
            set
            {
                if (value < _additionalValue)
                {
                    var remainder = _additionalValue - value;
                    
                    _additionalValue = 0;
                    _baseValue -= remainder;
                    _baseValue = Mathf.Max(_baseValue, 0);
                }
                else
                {
                    _baseValue = value;
                    _baseValue = Mathf.Clamp(_baseValue, MinValue, MaxValue);
                }
            }
        }

        public void AddModifier(AttributeModifier modifier)
        {
            _isDirty = true;
            _modifiers.Add(modifier);
        }

        public bool TryRemoveModifier(AttributeModifier modifier)
        {
            var value = _additionalValue;

            value -= modifier.Value;

            _additionalValue = Mathf.Max(value, 0);
                
            return _modifiers.Remove(modifier);
        }
        

        private float CalculateAdditionalValue()
        {
            var value = _baseValue;
            
            foreach (var modifier in _modifiers)
            {
                value += modifier.Value;
            }

            return value;
        }
    }
}