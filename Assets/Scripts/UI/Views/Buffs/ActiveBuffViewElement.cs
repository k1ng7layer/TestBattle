using Game.Models.Buffs;
using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;

namespace UI.Views.Buffs
{
    public class ActiveBuffViewElement : UiView
    {
        [SerializeField] private TextMeshProUGUI buffNameText;
        [SerializeField] private TextMeshProUGUI buffLifeTimeText;
        
        public EBuffType BuffType { get; private set; }
        
        public void SetBuffName(string buffName, EBuffType buffType)
        {
            buffNameText.text = buffName;
            
            BuffType = buffType;
        }

        public void SetBuffTick(int tick)
        {
            buffLifeTimeText.text = tick.ToString();
        }
    }
}