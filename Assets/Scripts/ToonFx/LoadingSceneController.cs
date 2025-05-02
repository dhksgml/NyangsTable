using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToonFx
{
    public class LoadingSceneController : MonoBehaviour
    {
        public static string nextSceneName = "Stage0"; // 전환할 씬 이름
        public float loadingTime = 5f;          // 로딩 대기 시간

        void Start()
        {
            StartCoroutine(LoadSceneAsync());
        }

        IEnumerator LoadSceneAsync()
        {
            // 단순히 대기 시간 후 씬 로드
            yield return new WaitForSeconds(loadingTime);

            // 비동기 씬 로딩
            AsyncOperation async = SceneManager.LoadSceneAsync(nextSceneName);

            while (!async.isDone)
            {
                yield return null;
            }
        }
    }
}