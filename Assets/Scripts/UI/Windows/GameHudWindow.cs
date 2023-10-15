using SimpleUi;
using UI.Controllers;
using UI.Controllers.Restart;
using UI.Controllers.Round;

namespace UI.Windows
{
    public class GameHudWindow : WindowBase
    {
        public override string Name => "GameHud";
        
        protected override void AddControllers()
        {
            AddController<RestartGameController>();
            AddController<RoundCounterController>();
            AddController<GameUiMainController>();
        }
    }
}