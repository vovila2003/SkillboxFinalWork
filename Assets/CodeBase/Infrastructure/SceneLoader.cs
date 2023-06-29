using System;
using CodeBase.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly LoadingCurtain _loadingCurtain;
        public SceneLoader(LoadingCurtain loadingCurtain) => 
            _loadingCurtain = loadingCurtain;

        public async void Load(string name, Action onLoaded = null) {
            if (SceneManager.GetActiveScene().name == name) {
                onLoaded?.Invoke();
                return;
            }
            _loadingCurtain.Value = 0;
            // var waitNextScene = SceneManager.LoadSceneAsync(name);
            // await UniTask.WaitUntil(() => waitNextScene.isDone);
            await SceneManager.LoadSceneAsync(name).ToUniTask(Progress.Create<float>(x => _loadingCurtain.Value = x));
            onLoaded?.Invoke();
        }
    }
}