using System.Collections.Generic;
using Game.Models.Buffs;
using Game.Presenters.Unit;
using Game.Services.Round;
using UI.Controllers.Abstractions;
using UI.Views.Buffs;
using Zenject;

namespace UI.Controllers.Combat
{
    public class ActiveBuffsController : UnitLinkableController<ActiveBuffsView>
    { 
        [Inject] private readonly IRoundProvider _roundProvider;
        
        private readonly Dictionary<EBuffType, ActiveBuffViewElement> _activeBuffsViewElements = new();

        public ActiveBuffsController(
            ActiveBuffsView view, 
            IUnit unit
        ) : base(view, unit)
        {
            
        }

        public override void Initialize()
        {
            Unit.Buffed += OnBuffed;
            Unit.BuffExpired += OnBuffExpired;
            _roundProvider.RoundChanged += UpdateBuffInfo;
        }

        private void OnBuffed(Buff buff)
        {
            var buffView = View.ActiveBuffCollection.Create();
            _activeBuffsViewElements.Add(buff.BuffType, buffView);
            
            buffView.SetBuffName(buff.BuffType.ToString(), buff.BuffType);
            buffView.SetBuffTick(buff.TickLeft);
        }   

        private void OnBuffExpired(Buff buff)
        {
            var buffView = _activeBuffsViewElements[buff.BuffType];
            _activeBuffsViewElements.Remove(buff.BuffType);
            
            View.ActiveBuffCollection.Remove(buffView);
        }

        private void UpdateBuffInfo(int _)
        {
            foreach (var buffView in _activeBuffsViewElements)
            {
                var buff = Unit.StaticBuffs[buffView.Key];
                
                buffView.Value.SetBuffTick(buff.TickLeft);
            }
        }
    }
}