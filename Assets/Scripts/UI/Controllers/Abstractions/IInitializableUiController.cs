using System;

namespace UI.Controllers.Abstractions
{
    public interface IInitializableUiController : IDisposable
    {
        void Initialize();
    }
}