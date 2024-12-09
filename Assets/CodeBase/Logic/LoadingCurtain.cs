using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        private float _fadeDuration = 0.5f;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Hide();
        }

        public void Show()
        {
            _curtain.alpha = 1;
            _curtain.blocksRaycasts = true;
        }

        public void HideInstant()
        {
            _curtain.alpha = 0;
            _curtain.blocksRaycasts = false;
        }

        private void Hide()
        {
            StartCoroutine(FadeOut());
        }
        
        private IEnumerator FadeOut()
        {
            float elapsedTime = 0f;
            float startAlpha = _curtain.alpha;

            while (elapsedTime < _fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                _curtain.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / _fadeDuration);
                yield return null;
            }

            _curtain.alpha = 0f;
            _curtain.blocksRaycasts = false;
        }
    }
}