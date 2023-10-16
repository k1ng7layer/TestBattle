using Game.Presenters.Unit;
using UI.Controllers.Abstractions;
using UI.Views.Stats;

namespace UI.Controllers.Stats
{
    public class UnitStatsController : UnitLinkableController<UnitStatsView>
    {
        public UnitStatsController(UnitStatsView view, IUnit unit) : base(view, unit)
        {
        }

        public override void Initialize()
        {
            SubscribeOnStatsUpdate(Unit);
            UpdateStat(Unit);
        }
        
        private void SubscribeOnStatsUpdate(IUnit unit)
        {
            foreach (var attributeKvp in unit.Attributes)
            {
                var attribute = attributeKvp.Value;
               
                attribute.Changed += Update;
                
                continue;

                void Update(float maxValue, float value)
                {
                    var percents = value / maxValue;
                 
                    View.UpdateStat(attributeKvp.Key, value, percents);
                }
            }
        }
        
        private void UpdateStat(IUnit unit)
        {
            foreach (var attributeKvp in unit.Attributes)
            {
                var attribute = attributeKvp.Value;
                var percents = attribute.Value / attribute.MaxValue;
                View.UpdateStat(attributeKvp.Key, attribute.Value, percents);
            }
        }
    }
}