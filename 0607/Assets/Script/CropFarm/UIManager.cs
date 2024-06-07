using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public Camera cropCamera;
    private RaycastHit hit;
    public TextMeshProUGUI moistLevelText, growthLevelText, nameText, fertilizerText;
    public TextMeshProUGUI countSeedWheatText, countSeedCarrotText, countSeedCornText;
    public Button harvestButton;

    public Canvas StateCanvas, CropCanvas;
    private ICrop clickedCrop, lastSelectedCrop;
    

    void Start()
    {
        StateCanvas.gameObject.SetActive(false);
        CropCanvas.gameObject.SetActive(false);
        harvestButton.gameObject.SetActive(false);
        lastSelectedCrop = null;

    }

    void Update()
    {
        ShowCountSeed();        
        if (Input.GetMouseButtonDown(0))
        {
            CheckCropClick();            
        }

        UpdateStateText();        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeMode();
        }
    }

    private void CheckCropClick()
    {
        Ray ray = cropCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {            
            clickedCrop = hit.transform.GetComponent<ICrop>();
            if (hit.transform == null) return;
            if (clickedCrop != null) lastSelectedCrop = clickedCrop;
            
            if (GameManager.instance.clickedWaterPot)
            {
                UpMoistLevel();
            }
            if (GameManager.instance.clickedFertilizer)
            {
                UpFertilizerLevel();
            }
        }
        if (hit.transform == null || lastSelectedCrop == null) return;
        if (hit.transform.name.Contains("seed"))
        {            
            ShowStateCanvas();
        }
    }


    // 식물 정보창 갱신
    private void UpdateStateText()
    {
        if (lastSelectedCrop == null) return;

        nameText.text = lastSelectedCrop.cropName;
        if (lastSelectedCrop.growthLevel <= 0)
        {
            fertilizerText.text = "성장";
            moistLevelText.text = "성장";
            growthLevelText.text = "성장";
            harvestButton.gameObject.SetActive(true);
        }
        else
        {
            moistLevelText.text = lastSelectedCrop.moistLevel.ToString();
            fertilizerText.text = lastSelectedCrop.fertilizerLevel.ToString();
            growthLevelText.text = $"{lastSelectedCrop.growthLevel / 60}m {lastSelectedCrop.growthLevel % 60}s";
        }        
    }


    // 남은 씨앗 수량 출력
    private void ShowCountSeed()
    {
        countSeedWheatText.text = GameManager.instance.seedCount[0].ToString();
        countSeedCarrotText.text = GameManager.instance.seedCount[1].ToString();
        countSeedCornText.text = GameManager.instance.seedCount[2].ToString();
    }

    public void ClickWaterPot()
    {
        GameManager.instance.clickedWaterPot = true;
        GameManager.instance.clickedFertilizer = false;        
    }
    public void ClickedFertilizer()
    {
        GameManager.instance.clickedWaterPot = false;
        GameManager.instance.clickedFertilizer = true;        
    }

    public void CloseStateCanvas() => StateCanvas.gameObject.SetActive(false);
    public void ShowCropCanvas() => CropCanvas.gameObject.SetActive(true);
    public void CloseCropCanvas() => CropCanvas.gameObject.SetActive(false);

    public void ShowStateCanvas()
    {
        StateCanvas.gameObject.SetActive(true);
    }

    public void OnClickHarvest()
    {       
        lastSelectedCrop.OnClickHarvest();
        CloseStateCanvas();
    }


    public void EscapeMode()
    {
        GameManager.instance.clickedWaterPot = GameManager.instance.clickedFertilizer = false;
        lastSelectedCrop = null;
    }


    public void UpFertilizerLevel()
    {
        if (lastSelectedCrop == null || lastSelectedCrop.growthLevel <= 0) return;
        lastSelectedCrop.fertilizerLevel = Math.Min(lastSelectedCrop.fertilizerLevel + 1, 3);        
    }


    public void UpMoistLevel()
    {
        if (lastSelectedCrop == null || lastSelectedCrop.growthLevel <= 0) return;
        GameManager.money -= 10;
        lastSelectedCrop.moistLevel = Math.Min(lastSelectedCrop.moistLevel + 10, 100);
    }

    public void OnOtherBtnClick()
    {
        GameManager.instance.clickedWaterPot = false;
        GameManager.instance.clickedFertilizer = false;
    }
}