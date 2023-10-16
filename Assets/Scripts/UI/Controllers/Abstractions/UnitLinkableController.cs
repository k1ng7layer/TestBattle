using System;
using Game.Presenters.Unit;
using SimpleUi.Abstracts;

namespace UI.Controllers.Abstractions
{
    public abstract class UnitLinkableController<TView> : IInitializableUiController, 
        IDisposable where TView : UiView
    {
        public UnitLinkableController(TView view, IUnit unit)
        {
            View = view;
            Unit = unit;
        }
        
        protected TView View { get; }
        protected IUnit Unit { get; }

        public abstract void Initialize();

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        { }
    }
}