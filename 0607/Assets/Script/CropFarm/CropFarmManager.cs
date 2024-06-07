using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CropFarmManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject waterPot, fertilizer;
    public Canvas[] canvases;
    public bool clickedWaterPot, clickedFertilizer;
    public static CropFarmManager Instance = null;

    void Update()
    {
        clickedWaterPot = GameManager.instance.clickedWaterPot;
        clickedFertilizer = GameManager.instance.clickedFertilizer;

        UpdateWaterPotState();
        UpdateFertilizerState();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideAllCanvases();
        }
    }

    private void UpdateWaterPotState()
    {
        waterPot.SetActive(clickedWaterPot);
    }

    private void UpdateFertilizerState()
    {
        fertilizer.SetActive(clickedFertilizer);
    }

    private void HideAllCanvases()
    {
        foreach (Canvas canvas in canvases)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}