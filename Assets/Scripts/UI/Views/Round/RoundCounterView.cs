using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;

namespace UI.Views.Round
{
    public class RoundCounterView : UiView
    {
        [SerializeField] private TextMeshProUGUI roundText;

        public void SetRoundCounter(int roundNum)
        {
            roundText.text = $"Round {roundNum}";
        }
    }
}