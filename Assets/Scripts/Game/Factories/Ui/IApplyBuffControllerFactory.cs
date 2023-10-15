using Game.Presenters.Unit;
using UI.Controllers.Combat;
using UI.Views.Combat;
using Zenject;

namespace Game.Factories.Ui
{
    public interface IApplyBuffControllerFactory : IFactory<ApplyBuffButtonView, IUnit, ApplyBuffController>
    {
        
    }
}