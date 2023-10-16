using Game.Presenters.Unit;

namespace Game.Services.AttackManager
{
    public interface IAttackManager
    {
        void DoAttack(IUnit attacker, IUnit target);
        void ApplyBuff(IUnit attacker);
    }
}