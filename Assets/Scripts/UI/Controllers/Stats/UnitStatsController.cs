using System;
using Game.Presenters.Unit;
using UI.Controllers.Abstractions;
using UI.Views.Stats;

namespace UI.Controllers.Stats
{
    public class UnitStatsController : UnitLinkableController<UnitStatsView>, IDisposable
    {
        public UnitStatsController(
            UnitStatsView view, 
            IUnit unit) : base(view, unit)
        {
        }

        public override void Initialize()
        {
            SubscribeOnUpdate(Unit);
            UpdateStat(Unit);
        }
        
        private void SubscribeOnUpdate(IUnit unit)
        {
            foreach (var attribute in unit.Attributes)
            {
                attribute.Value.Changed += Update;
                continue;

                void Update(float value)
                {
                    View.UpdateStat(attribute.Key, value);
                }
            }
        }
        
        private void UpdateStat(IUnit unit)
        {
            foreach (var attribute in unit.Attributes)
            {
                View.UpdateStat(attribute.Key, attribute.Value.Value);
            }
        }

        public void Dispose()
        {
            
        }
    }
}