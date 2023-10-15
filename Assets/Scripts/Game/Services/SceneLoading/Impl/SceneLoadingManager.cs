using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Services.SceneLoading.Impl
{
    public class SceneLoadingManager : ISceneLoadingManager
    {
        public void RestartGame()
        {
            // var unloadingOperation = SceneManager.UnloadSceneAsync(ELevelName.Game.ToString());
            // Observable.FromCoroutine(() => UnLoadScene(unloadingOperation))
            //     .Subscribe(_ =>
            //     {
            //         // var loadingOperation =
            //         //     SceneManager.LoadSceneAsync(ELevelName.Game.ToString(), LoadSceneMode.Single);
            //         //
            //         // var disposable = Observable.FromCoroutine(() => LoadScene(loadingOperation));
            //         //.Subscribe(_ => RunContext("GameContext"));
            //     });
            
            var loadingOperation =
                SceneManager.LoadSceneAsync(ELevelName.Game.ToString(), LoadSceneMode.Single);

            var disposable = Observable.FromCoroutine(() => LoadScene(loadingOperation));

        }

        private IEnumerator LoadScene(AsyncOperation loadingOperation)
        {
            loadingOperation.allowSceneActivation = true;
            
            while (!loadingOperation.isDone)
            {
                yield return null;
            }
        }
        
        private IEnumerator UnLoadScene(AsyncOperation loadingOperation)
        {
            //loadingOperation.allowSceneActivation = true;
            
            while (!loadingOperation.isDone)
            {
                yield return null;
            }
        }
        

        private void RunContext(string name)
        {
            var contexts = Object.FindObjectsOfType<SceneContext>();
            foreach (var context in contexts)
            {
                if (context.gameObject.name != name)
                    continue;
                context.Run();
                return;
            }
        }
    }
}