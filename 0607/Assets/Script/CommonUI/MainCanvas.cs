using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MainCanvas : MonoBehaviour
{
    public TextMeshProUGUI TimeText;
    public static MainCanvas Instance = null;
    public TextMeshProUGUI moneyText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        UpdateTimeText();
        UpdateMoneyText();
    }

    private void UpdateTimeText()
    {
        DateTime now = DateTime.Now;
        TimeText.text = now.ToString("MM/dd ddd HH:mm:ss", CultureInfo.InvariantCulture);
    }

    private void UpdateMoneyText()
    {
        moneyText.text = GameManager.money.ToString();
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
