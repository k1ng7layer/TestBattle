using System.Globalization;
using Game.Models.Attributes;
using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;

namespace UI.Views.Stats
{
    public class UnitStatsView : UiView
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI armorText;
        [SerializeField] private TextMeshProUGUI attackDamageText;
        [SerializeField] private TextMeshProUGUI vampirismDamageText;

        public void UpdateStat(EAttributeType attributeType, float value)
        {
            var textField = GetTextField(attributeType);
            
            textField.text = value.ToString(CultureInfo.InvariantCulture);
        }

        private TextMeshProUGUI GetTextField(EAttributeType attributeType)
        {
            return attributeType switch
            {
                EAttributeType.Health => healthText,
                EAttributeType.Armor => armorText,
                EAttributeType.Vampirism => vampirismDamageText,
                EAttributeType.AttackDamage => attackDamageText,
            };
        }
    }
}