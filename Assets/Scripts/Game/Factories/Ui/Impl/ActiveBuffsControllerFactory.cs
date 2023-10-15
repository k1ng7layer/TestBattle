using Game.Presenters.Unit;
using UI.Controllers.Combat;
using UI.Views.Buffs;
using Zenject;

namespace Game.Factories.Ui.Impl
{
    public class ActiveBuffsControllerFactory : PlaceholderFactory<ActiveBuffsView, IUnit, ActiveBuffsController>,
        IActiveBuffsControllerFactory
    {
        
    }
}