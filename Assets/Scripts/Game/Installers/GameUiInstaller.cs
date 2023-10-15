using SimpleUi;
using UI.Controllers;
using UI.Controllers.Restart;
using UI.Controllers.Round;
using UI.Views.Combat;
using UI.Views.Restart;
using UI.Views.Round;
using UI.Views.Stats;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameUiInstaller), fileName = nameof(GameUiInstaller))]
    public class GameUiInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private UnitStatsView unitStatsView;
        [SerializeField] private StatsMainView statsMainView;
        [SerializeField] private CombatMainView combatMainView;
        [SerializeField] private RestartGameButtonView restartGameButton;
        [SerializeField] private ApplyBuffMainView applyBuffMainView;
        [SerializeField] private RoundCounterView roundCounterView;
        [SerializeField] private GameUiMainView gameUiMainView;
        
        public override void InstallBindings()
        {
            var canvasView = Container.InstantiatePrefabForComponent<Canvas>(canvas);
            var canvasTransform = canvasView.transform;
            
            // Container.Bind<UnitStatsView>()
            //     .FromNewComponentOnNewPrefab(unitStatsView)
            //     .UnderTransform(canvasTransform)
            //     .AsTransient()
            //     .OnInstantiated((context, o) => ((MonoBehaviour) o).gameObject.SetActive(false));
            
            //Container.BindUiView<UnitStatsMainController, StatsMainView>(statsMainView, canvasTransform);
            //Container.BindUiView<AttackButtonsMainController, CombatMainView>(combatMainView, canvasTransform);
            Container.BindUiView<RestartGameController, RestartGameButtonView>(restartGameButton, canvasTransform);
            //Container.BindUiView<ApplyBuffMainController, ApplyBuffMainView>(applyBuffMainView, canvasTransform);
            Container.BindUiView<RoundCounterController, RoundCounterView>(roundCounterView, canvasTransform);
            Container.BindUiView<GameUiMainController, GameUiMainView>(gameUiMainView, canvasTransform);
        }
    }
}