using SimpleUi;
using UI.Controllers;
using UI.Controllers.Restart;
using UI.Controllers.Round;
using UI.Views.Restart;
using UI.Views.Round;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameUiInstaller), fileName = nameof(GameUiInstaller))]
    public class GameUiInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private RestartGameButtonView restartGameButton;
        [SerializeField] private RoundCounterView roundCounterView;
        [SerializeField] private GameUiMainView gameUiMainView;
        
        public override void InstallBindings()
        {
            var canvasView = Container.InstantiatePrefabForComponent<Canvas>(mainCanvas);
            var canvasTransform = canvasView.transform;
            
   
            Container.BindUiView<RestartGameController, RestartGameButtonView>(restartGameButton, canvasTransform);
            Container.BindUiView<RoundCounterController, RoundCounterView>(roundCounterView, canvasTransform);
            Container.BindUiView<GameUiMainController, GameUiMainView>(gameUiMainView, canvasTransform);
        }
    }
}