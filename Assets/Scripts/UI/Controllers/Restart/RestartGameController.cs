using Game.Services.SceneLoading;
using SimpleUi.Abstracts;
using UI.Views.Restart;
using UniRx;
using Zenject;

namespace UI.Controllers.Restart
{
    public class RestartGameController : UiController<RestartGameButtonView>, IInitializable
    {
        private readonly ISceneLoadingManager _sceneLoadingManager;

        public RestartGameController(ISceneLoadingManager sceneLoadingManager)
        {
            _sceneLoadingManager = sceneLoadingManager;
        }

        public void Initialize()
        {
            View.RestartGameButton.OnClickAsObservable().Subscribe(_ => RestartGame()).AddTo(View);
        }

        private void RestartGame()
        {
            _sceneLoadingManager.RestartGame();
        }
    }
}