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
        public event Action<float, float> Changed; 

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
                    _additionalValue = 0;
                
                _baseValue = value;
                _baseValue = Mathf.Clamp(_baseValue, MinValue, MaxValue);
                
                Changed?.Invoke(MaxValue, Value);
            }
        }

        public void AddModifier(AttributeModifier modifier)
        {
            _isDirty = true;
            _modifiers.Add(modifier);
            
            Changed?.Invoke(MaxValue, Value);
        }

        public bool TryRemoveModifier(AttributeModifier modifier)
        {
            var removed = _modifiers.Remove(modifier);
            
            var value = _additionalValue;

            if (_additionalValue > 0 && removed)
            {
                switch (modifier.ModifierType)
                {
                    case EModifierType.Divide:
                        value += _baseValue;
                        value *= modifier.Value;
                        value -= _baseValue;
                        break;
                    case EModifierType.Multiply:
                        value += _baseValue;
                        value /= modifier.Value;
                        _additionalValue -= value;
                        break;
                    case EModifierType.Add:
                        value -= modifier.Value;
                        _additionalValue = value;
                        break;
                    case EModifierType.Substract:
                        value += modifier.Value;
                        _additionalValue = value;
                        break;
                }
            }
            
            _additionalValue = Mathf.Max(_additionalValue, 0);
            Changed?.Invoke(MaxValue, Value);

            return removed;
        }

        private float CalculateValue()
        {
            float value = 0;
            
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
                    case EModifierType.Substract:
                        value -= modifier.Value;
                        break;
                }
                
                var totalValue = value + _baseValue;

                if (totalValue > MaxValue)
                {
                    var excess = totalValue - MaxValue;
                    value -= excess;
                }

                if (totalValue < MinValue)
                {
                    var excess = totalValue - MinValue;
                    value -= excess;
                }
            }
            
            return value;
        }
    }
}