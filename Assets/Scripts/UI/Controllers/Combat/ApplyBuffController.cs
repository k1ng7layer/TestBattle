using System;
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
    public class ApplyBuffController : UnitLinkableController<ApplyBuffButtonView>, IDisposable
    {
        [Inject] private readonly ICombatManager _combatManager;
        [Inject] private readonly IAttackQueueService _attackQueueService;

        public ApplyBuffController(
            ApplyBuffButtonView view, 
            IUnit unit) : base(view, unit)
        {
            
        }

        public override void Initialize()
        {
            View.applyButton.OnClickAsObservable().Subscribe(_ => AddBuff()).AddTo(View);
            _attackQueueService.ActiveUnitChanged += OnAttackUnitChanged;
            View.SetState(_attackQueueService.ActiveMember.Unit == Unit);
        }

        private void AddBuff()
        {
            _combatManager.ApplyBuff();
        }
        
        private void OnAttackUnitChanged(BattleMember battleMember)
        {
           View.SetState(battleMember.Unit == Unit);
        }

        public void Dispose()
        {
            _attackQueueService.ActiveUnitChanged -= OnAttackUnitChanged;
        }
    }
}