using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StateManager : MonoBehaviour
{
    public GameObject PausePanel;
    public Camera mainCamera;
    bool clickedWaterPot;
    public Canvas StateCanvas;


    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}