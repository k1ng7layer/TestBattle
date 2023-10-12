using System.Collections.Generic;
using Game.Models.Modifiers;
using Game.Presenters.Unit;

namespace Game.Models.Buffs
{
    public abstract class BuffBase
    {
        private readonly int _lifeTime;
        private int _tick;
        
        protected IUnit Target { get; private set; }
        protected List<AttributeModifier> BuffModifiers { get; } = new();
        
        public abstract EBuffType BuffType { get; }
        public abstract int LifeTime { get; }

        public void Apply(IUnit target)
        {
            Target = target;
            
            OnApply(BuffModifiers);

            foreach (var buffModifier in BuffModifiers)
            {
                if (Target.Attributes.TryGetValue(buffModifier.AttributeType, out var attribute))
                {
                    attribute.AddModifier(buffModifier);
                }
            }
        }

        protected abstract void OnApply(List<AttributeModifier> attributeModifiers);

        private void Disable()
        {
            OnDisable();
            
            Target.TryRemoveBuff(this);
            
            foreach (var buffModifier in BuffModifiers)
            {
                if (Target.Attributes.TryGetValue(buffModifier.AttributeType, out var attribute))
                {
                    attribute.TryRemoveModifier(buffModifier);
                }
            }
        }
        
        public void Tick()
        {
            _tick++;
            
            if(_tick == _lifeTime)
                Disable();
        }

        protected virtual void OnDisable()
        { }
    }
}