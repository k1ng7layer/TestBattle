using System;
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
        
        public CharacterAttribute(float value, float minValue, float maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            _baseValue = value;
        }
        
        public float MaxValue { get; }
        public float MinValue { get; }
        public event Action<float> Changed; 

        public float Value
        {
            get
            {
                if (_isDirty)
                {
                    _additionalValue = CalculateValue();
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
                
                Changed?.Invoke(Value);
            }
        }

        public void AddModifier(AttributeModifier modifier)
        {
            _isDirty = true;
            _modifiers.Add(modifier);
            
            Changed?.Invoke(Value);
        }

        public bool TryRemoveModifier(AttributeModifier modifier)
        {
            var value = _additionalValue;

            value -= modifier.Value;

            _additionalValue = Mathf.Max(value, 0);
                
            return _modifiers.Remove(modifier);
        }
        

        private float CalculateValue()
        {
            var value = _additionalValue;
            
            foreach (var modifier in _modifiers)
            {
                switch (modifier.ModifierType)
                {
                    case EModifierType.Divide:
                        value += _baseValue;
                        value /= modifier.Value;
                        break;
                    case EModifierType.Multiply:
                        value += _baseValue;
                        value *= modifier.Value;
                        value -= _baseValue;
                        break;
                    case EModifierType.Add:
                        value += modifier.Value;
                        break;
                    case EModifierType.AddPercents:
                        value += _baseValue;
                        value *= 1 + modifier.Value;
                        value -= _baseValue;
                        break;
                    case EModifierType.Substract:
                        value -= modifier.Value;
                        break;
                }
            }

            return value;
        }
    }
}