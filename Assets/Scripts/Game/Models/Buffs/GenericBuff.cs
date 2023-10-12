using System.Collections.Generic;
using Game.Models.Modifiers;
using Game.Presenters.Unit;

namespace Game.Models.Buffs
{
    public class GenericBuff
    {
        private readonly IEnumerable<AttributeModifier> _attributeModifiers;

        private readonly int _lifeTime;
        private int _tick;
        
        public GenericBuff(
            IEnumerable<AttributeModifier> attributeModifiers, 
            EBuffType buffType, 
            int lifeTime)
        {
            _attributeModifiers = attributeModifiers;
            BuffType = buffType;
            _lifeTime = lifeTime;
        }
        
        public IUnit Target { get; private set; }
        public EBuffType BuffType { get; }

        public void Apply(IUnit target)
        {
            Target = target;
            //Target.AddBuff(this);

            foreach (var modifier in _attributeModifiers)
            {
                if (Target.Attributes.TryGetValue(modifier.AttributeType, out var attribute))
                {
                    attribute.AddModifier(modifier);
                }
            }
        }

        public void Disable()
        {
            foreach (var modifier in _attributeModifiers)
            {
                if (Target.Attributes.TryGetValue(modifier.AttributeType, out var attribute))
                {
                    attribute.TryRemoveModifier(modifier);
                }
            }
            
            //Target.RemoveBuff(this);
        }
        
        public void Tick()
        {
            _tick++;
            
            if(_tick == _lifeTime)
                Disable();
        }
    }
}