using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject PausePanel;
    public TextMeshProUGUI wheatCountText, wheatSeedCountText, carrotCountText,
        carrotSeedCountText, cornSeedCountText, cornCountText;

    public TextMeshProUGUI wheatPriceText, wheatSeedPriceText, carrotPriceText,
        carrotSeedPriceText, cornSeedPriceText, cornPriceText;

    public Canvas equipmentCanvas, cropCanvas, animalCanvas;

    private void Start()
    {
        // 모든 캔버스 비활성화
        equipmentCanvas.enabled = false;
        cropCanvas.enabled = false;
        animalCanvas.enabled = false;
    }

    private void Update()
    {
        //텍스트 갱신
        UpdateCountText();
    }

    private void UpdateCountText()
    {
        wheatSeedCountText.text = GameManager.instance.seedCount[0].ToString();
        wheatSeedPriceText.text = GameManager.instance.seedPrice[0].ToString();
        wheatCountText.text = GameManager.instance.cropCount[0].ToString();
        wheatPriceText.text = GameManager.instance.cropPrice[0].ToString();

        carrotSeedCountText.text = GameManager.instance.seedCount[1].ToString();
        carrotSeedPriceText.text = GameManager.instance.seedPrice[1].ToString();
        carrotCountText.text = GameManager.instance.cropCount[1].ToString();
        carrotPriceText.text = GameManager.instance.cropPrice[1].ToString();

        cornSeedCountText.text = GameManager.instance.seedCount[2].ToString();
        cornSeedPriceText.text = GameManager.instance.seedPrice[2].ToString();
        cornCountText.text = GameManager.instance.cropCount[2].ToString();
        cornPriceText.text = GameManager.instance.cropPrice[2].ToString();
    }

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

    // 캔버스 활성화
    public void SetActiveCanvas(Canvas activeCanvas)
    {
        equipmentCanvas.enabled = activeCanvas == equipmentCanvas;
        cropCanvas.enabled = activeCanvas == cropCanvas;
        animalCanvas.enabled = activeCanvas == animalCanvas;
    }
}
