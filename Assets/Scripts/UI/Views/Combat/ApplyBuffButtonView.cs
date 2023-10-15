using SimpleUi.Abstracts;
using UnityEngine.UI;

namespace UI.Views.Combat
{
    public class ApplyBuffButtonView : UiView
    {
        public Button applyButton;
        
        public void SetState(bool active)
        {
            applyButton.interactable = active;
        }
    }
}