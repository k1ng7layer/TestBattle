using System.Collections;
using UnityEngine;

namespace Game.Views.Unit.Impl
{
    public class UnitView : MonoBehaviour, 
        IUnitView
    {
        [SerializeField] private Renderer meshRenderer;
        [SerializeField] private Color targetColor;
        [SerializeField] private float fadeDuration;

        private Color _initialColor;

        private void Start()
        {
            _initialColor = meshRenderer.material.color;
        }

        public void OnTakeDamage()
        {
            StopAllCoroutines();
            
            StartCoroutine(ColorBlink(_initialColor, targetColor));
        }

        private IEnumerator ColorBlink(Color from, Color to)
        {
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration / 2f)
            {
                elapsedTime += Time.deltaTime;
                meshRenderer.material.color = Color.Lerp(from, to, elapsedTime / (fadeDuration /2 ));
                yield return null;
            }
            
            elapsedTime = 0f;
            
            while (elapsedTime < fadeDuration / 2f)
            {
                elapsedTime += Time.deltaTime;
                meshRenderer.material.color = Color.Lerp(to, from, elapsedTime / (fadeDuration /2 ));
                yield return null;
            }
        }
    }
}