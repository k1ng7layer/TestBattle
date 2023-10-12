using Game.Presenters.Unit;

namespace Game.Models.Buffs.Buffs2
{
    public abstract class AttackBuff : BuffBase2
    {
        protected override void OnApply(IUnit target)
        {
            foreach (var buffModifier in BuffModifiers)
            {
                target.AddAttackModifier(buffModifier);
            }
        }

        protected override void OnDisable(IUnit target)
        {
            foreach (var buffModifier in BuffModifiers)
            {
                target.RemoveAttackModifier(buffModifier);
            }
        }
    }
}