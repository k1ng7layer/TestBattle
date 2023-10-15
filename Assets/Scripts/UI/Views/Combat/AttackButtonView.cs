using SimpleUi.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.Combat
{
    public class AttackButtonView : UiView
    {
        [SerializeField] public Button attackButton;

        public void SetState(bool active)
        {
            attackButton.interactable = active;
        }
    }
}