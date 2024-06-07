using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour
{
    public Button homeButton;
    private static DontDestroy instance;

    void Awake()
    {
        // 싱글톤 패턴
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 씬이 로드될 때마다 호출
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬에 따른 homeButton 활성화/비활성화
        if (scene.name == "GameScene")
        {
            homeButton.gameObject.SetActive(false);
        }
        else
        {
            homeButton.gameObject.SetActive(true);
        }
    }

    // 이 객체가 파괴될 때 씬 로드 이벤트 등록 해제
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
