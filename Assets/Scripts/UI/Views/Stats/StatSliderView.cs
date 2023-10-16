using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.Stats
{
    public class StatSliderView : MonoBehaviour
    {
        [SerializeField] private Color sliderColor;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private Image image;

        private void Start()
        {
            image.color = sliderColor;
        }

        public void Set(string title, float textValue, float sliderValue)
        {
            valueText.text = $"{title} {textValue}";
            image.fillAmount = sliderValue;
        }
    }
}