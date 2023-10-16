using Game.Models.Attributes;
using SimpleUi.Abstracts;
using UnityEngine;

namespace UI.Views.Stats
{
    public class UnitStatsView : UiView
    {
        [SerializeField] private StatSliderView healthSlider;
        [SerializeField] private StatSliderView armorSlider;
        [SerializeField] private StatSliderView attackDamageSlider;
        [SerializeField] private StatSliderView vampirismSlider;

        public void UpdateStat(EAttributeType attributeType, 
            float value, 
            float percents)
        {
            var slider = GetSlider(attributeType);
            var title = attributeType.ToString();
            // Debug.Log($"set slider {attributeType}, value = {value}, percents = {percents}");
            slider.Set(title, value, percents);
        }

        private StatSliderView GetSlider(EAttributeType attributeType)
        {
            return attributeType switch
            {
                EAttributeType.Health => healthSlider,
                EAttributeType.Armor => armorSlider,
                EAttributeType.Vampirism => vampirismSlider,
                EAttributeType.AttackDamage => attackDamageSlider,
            };
        }
    }
}