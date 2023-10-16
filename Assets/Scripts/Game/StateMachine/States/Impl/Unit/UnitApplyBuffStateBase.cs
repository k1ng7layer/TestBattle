using Game.Presenters.Unit;
using Game.Services.Buffs;
using Game.Settings.Battle;
using Game.StateMachine.StateMachine.Impl;
using Zenject;

namespace Game.StateMachine.States.Impl.Unit
{
    public class UnitApplyBuffStateBase : UnitStateBase
    {
        [Inject] private readonly IBattleSettingsBase _battleSettingsBase;
        [Inject] private readonly IBuffProvider _buffProvider;
        private readonly IUnit _unit;

        public UnitApplyBuffStateBase(
            IUnit unit,
            UnitStateMachine stateMachineBase
        ) : base(unit, stateMachineBase)
        {
            _unit = unit;
        }

        public override EBattleState StateName => EBattleState.ApplyBuff;
        
        protected override void OnExecute()
        {
            var hasMaxBuffs = _unit.StaticBuffs.Count == _battleSettingsBase.MaxActiveBuffs;

            if (!hasMaxBuffs)
            {
                var getBuff = _buffProvider.TryGetBuff(_unit.StaticBuffs);
                _unit.AddBuff(getBuff);
            }
            
            StateMachine.ChangeState(EBattleState.WaitForAttack);
        }
    }
}