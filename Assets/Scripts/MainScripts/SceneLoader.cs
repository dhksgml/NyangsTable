using UnityEngine;
using UnityEngine.SceneManagement; // 씬 로딩을 위해 필요
#if UNITY_EDITOR
using UnityEditor; // 유니티 에디터에서 종료할 때 필요
#endif

public class SceneLoader : MonoBehaviour
{
    // 씬 이름
    public string sceneToLoad;

    // 씬 로딩 함수
    public void LoadGame()
    {
        // 씬이 빌드 설정에 포함되어 있는지 확인
        if (IsSceneInBuildSettings(sceneToLoad))
        {
            Debug.Log(sceneToLoad + " 씬 로딩 시도");
            SceneManager.LoadScene(sceneToLoad); // 씬 로드
        }
        else
        {
            Debug.LogError(sceneToLoad + "' 씬이 Build Settings에 추가되지 않았습니다! 씬을 추가해 주세요.");
        }
    }

    public void LoadGame(string sceneName)
    {
        // 씬이 빌드 설정에 포함되어 있는지 확인
        if (IsSceneInBuildSettings(sceneName))
        {
            Debug.Log(sceneToLoad + " 씬 로딩 시도");
            ToonFx.LoadingSceneController.nextSceneName = sceneName;

            int randomNum = Random.Range(0, 2);
            string randomSceneName = randomNum == 0 ? "LoadingScene" : "LoadingScene2";
            SceneManager.LoadScene(randomSceneName); // 씬 로드
        }
        else
        {
            Debug.LogError(sceneName + "' 씬이 Build Settings에 추가되지 않았습니다! 씬을 추가해 주세요.");
        }
    }

    // 씬이 빌드 설정에 포함되어 있는지 확인하는 함수
    private bool IsSceneInBuildSettings(string sceneName)
    {
        // 빌드 설정에 포함된 씬들의 경로를 확인
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            // 씬 이름이 빌드 설정 목록에 포함되어 있는지 확인
            if (sceneFileName == sceneName)
            {
                return true;  // 해당 씬이 빌드 설정에 포함됨
            }
        }
        return false;  // 해당 씬이 빌드 설정에 포함되지 않음
    }

    // 게임 종료 함수
    public void QuitGame()
    {
        Debug.Log("게임 종료 시도"); // 종료 로그 출력

#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // 유니티 에디터에서 실행 중지
#else
        Application.Quit(); // 빌드된 게임에서는 정상 종료
#endif
    }
}
