using Game.Presenters.Unit;

namespace Game.Models.Buffs.Buffs2
{
    public abstract class StaticBuff : BuffBase2
    {
        private IUnit _target;
        
        protected override void OnApply(IUnit target)
        {
            _target = target;
            
            SetupModifiers(BuffModifiers);

            foreach (var buffModifier in BuffModifiers)
            {
                if (target.Attributes.TryGetValue(buffModifier.AttributeType, out var attribute))
                {
                    attribute.AddModifier(buffModifier);
                }
            }
        }

        protected override void OnDisable(IUnit target)
        {
            foreach (var buffModifier in BuffModifiers)
            {
                if (_target.Attributes.TryGetValue(buffModifier.AttributeType, out var attribute))
                {
                    attribute.TryRemoveModifier(buffModifier);
                }
            }
        }
    }
}