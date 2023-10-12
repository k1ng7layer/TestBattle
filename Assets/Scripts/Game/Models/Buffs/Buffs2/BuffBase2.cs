using System.Collections.Generic;
using Game.Models.Modifiers;
using Game.Presenters.Unit;

namespace Game.Models.Buffs.Buffs2
{
    public abstract class BuffBase2
    {
        private IUnit _target;
        
        protected abstract EBuffType BuffType { get; }
        protected List<AttributeModifier> BuffModifiers { get; } = new();

        public void Apply(IUnit target)
        {
            _target = target;
            
            SetupModifiers(BuffModifiers);
            
            OnApply(_target);
        }

        public void Disable()
        {
            OnDisable(_target);
        }

        protected abstract void SetupModifiers(List<AttributeModifier> attributeModifiers);

        protected virtual void OnApply(IUnit target)
        { }
        
        protected virtual void OnDisable(IUnit target)
        { }
    }
}