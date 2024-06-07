using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour
{
    public Button homeButton;
    private static DontDestroy instance;

    void Awake()
    {
        // �̱��� ����
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ���� �ε�� ������ ȣ��
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� ���� homeButton Ȱ��ȭ/��Ȱ��ȭ
        if (scene.name == "GameScene")
        {
            homeButton.gameObject.SetActive(false);
        }
        else
        {
            homeButton.gameObject.SetActive(true);
        }
    }

    // �� ��ü�� �ı��� �� �� �ε� �̺�Ʈ ��� ����
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
