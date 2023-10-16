using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;

namespace UI.Views.Buffs
{
    public class ActiveBuffViewElement : UiView
    {
        [SerializeField] private TextMeshProUGUI buffNameText;
        [SerializeField] private TextMeshProUGUI buffLifeTimeText;
        
        public string BuffName { get; private set; }
        
        public void SetBuffName(string buffName)
        {
            buffNameText.text = buffName;
            
            BuffName = buffName;
        }

        public void SetBuffTick(int tick)
        {
            buffLifeTimeText.text = tick.ToString();
        }
    }
}