using Game.Presenters.Unit;
using UI.Controllers.Combat;
using UI.Views.Buffs;
using Zenject;

namespace Game.Factories.Ui
{
    public interface IActiveBuffsControllerFactory : IFactory<ActiveBuffsView, IUnit, ActiveBuffsController>
    {
        
    }
}