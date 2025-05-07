using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToonFx
{
    public class LoadingSceneController : MonoBehaviour
    {
        public static string nextSceneName;  // 최종 목적지 씬 이름
        public float loadingTime = 5f;       // 로딩 시간

        void Start()
        {
            StartCoroutine(LoadSceneAsync());
        }

        IEnumerator LoadSceneAsync()
        {
            yield return new WaitForSeconds(loadingTime);

            AsyncOperation async = SceneManager.LoadSceneAsync(nextSceneName);

            while (!async.isDone)
            {
                yield return null;
            }
        }
    }
}