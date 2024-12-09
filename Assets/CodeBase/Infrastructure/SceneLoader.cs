using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using CodeBase.Utils.SmartDebug;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly DSender _sender = new("SceneLoader");
        private bool _isLoading;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName, System.Action onLoaded = null)
        {
            if (_isLoading) return;
            
            _isLoading = true;
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, () =>
            {
                _isLoading = false;
                onLoaded?.Invoke();
            }));
        }

        private IEnumerator LoadScene(string sceneName, System.Action onLoaded = null)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName);
            while (!waitNextScene.isDone)
            {
                DLogger.Message(_sender)
                    .WithText($"Loading progress: {waitNextScene.progress * 100}%")
                    .Log();
                yield return null;
            }
            
            onLoaded?.Invoke();
        }
    }
}