using Game.Battle;
using Game.Models.Combat;
using Game.Presenters.Unit;
using Game.Services.AttackQueueProvider;
using UI.Controllers.Abstractions;
using UI.Views.Combat;
using UniRx;
using Zenject;

namespace UI.Controllers.Combat
{
    public class PerformAttackButtonController : UnitLinkableController<AttackButtonView>
    {
        [Inject] private readonly IAttackQueueService _attackQueueService;
        [Inject] private readonly ICombatManager _combatManager;

        public PerformAttackButtonController(AttackButtonView view, IUnit unit) : base(view, unit)
        {
    
        }

        public override void Initialize()
        {
            View.attackButton.OnClickAsObservable().Subscribe(_ => Attack()).AddTo(View);
            _attackQueueService.ActiveUnitChanged += OnAttackUnitChanged;
            
            View.SetState(_attackQueueService.ActiveMember.Unit == Unit);
        }

        private void Attack()
        {
            _combatManager.Attack();
        }

        private void OnAttackUnitChanged(BattleMember battleMember)
        {
            View.SetState(battleMember.Unit == Unit);
        }
    }
}