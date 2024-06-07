using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BtnSoundManager : MonoBehaviour
{
    private static BtnSoundManager instance;
    public AudioClip buttonClickSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = buttonClickSound;

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AddClickSoundToAllButtons();
    }

    private void AddClickSoundToAllButtons()
    {
        List<Button> allButtons = new List<Button>();
        Transform[] allTransforms = FindObjectsOfType<Transform>(true);
        foreach (Transform trans in allTransforms)
        {
            Button button = trans.GetComponent<Button>();
            if (button != null)
            {
                allButtons.Add(button);
            }
        }

        foreach (Button button in allButtons)
        {
            button.onClick.AddListener(() => PlayButtonClickSound());
        }
    }

    private void PlayButtonClickSound()
    {
        audioSource.Play();
    }
}
