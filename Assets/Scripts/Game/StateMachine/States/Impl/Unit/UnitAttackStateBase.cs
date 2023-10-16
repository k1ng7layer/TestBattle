using Game.Models.Attributes;
using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;
using Zenject;

namespace Game.StateMachine.States.Impl.Unit
{
    public class UnitAttackStateBase : UnitStateBase
    {
        private readonly IUnit _target;
        [Inject] private readonly BattleStateMachine _battleStateMachine;

        public UnitAttackStateBase(
            IUnit attacker, 
            IUnit target, 
            UnitStateMachine stateMachineBase
        ) : base(attacker, stateMachineBase)
        {
            _target = target;
        }
        
        public override EBattleState StateName => EBattleState.Attack;
        
        protected override void OnExecute()
        {
            var damage = Unit.Attributes[EAttributeType.AttackDamage];
            
            var totalDamage = CalculateDamage(_target, damage.Value);
            
            Unit.PerformAttack();

            _target.TakeDamage(totalDamage, Unit.AttackModifiers);
            
            var targetHealth = _target.Attributes[EAttributeType.Health];
            
            if(targetHealth.Value <= 0)
                _battleStateMachine.ChangeState(EBattleState.BattleComplete);
            
            StateMachine.ChangeState(EBattleState.WaitForRoundEnd);
        }
        
        private float CalculateDamage(IUnit target, float damage)
        {
            var armor = target.Attributes[EAttributeType.Armor];
            var armorAffected = damage - damage / 100f * armor.Value;

            return armorAffected;
        }
    }
}