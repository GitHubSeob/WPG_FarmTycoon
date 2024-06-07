using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject PausePanel;

    public Canvas CropCanvas, AnimalCanvas;

    public TextMeshProUGUI[] seedCountText; // 씨앗 보유 개수
    public TextMeshProUGUI[] seedPriceText; // 씨앗 개당 가격
    public TextMeshProUGUI[] cropCountText; // 작물 보유 개수
    public TextMeshProUGUI[] cropPriceText; // 작물 개당 가격
    public TextMeshProUGUI[] seedTotalAmountText; // 씨앗 구매/판매 총 수량
    public TextMeshProUGUI[] cropTotalAmountText; // 작물 구매/판매 총 수량
    public TextMeshProUGUI[] seedTotalPriceText; // 씨앗 구매/판매 총 가격
    public TextMeshProUGUI[] cropTotalPriceText; // 작물 구매/판매 총 가격

    public Button[] plusBtn, minusBtn;
    public Button[] num1Btn, num5Btn, num10Btn;

    private enum Sign { None = 0, Plus = 1, Minus = -1 }
    private Sign[] sign = new Sign[6];
    private int[] seedAmount = new int[3];
    private int[] cropAmount = new int[3];

    void Start()
    {
        InitializeTexts();
        AddButtonListeners();
        AnimalCanvas.enabled = false;
    }

    private void Update()
    {
        UpdateShopUI();
    }

    private void InitializeTexts()
    {
        for (int idx = 0; idx < 3; ++idx)
        {
            seedTotalPriceText[idx].text = "0";
            cropTotalPriceText[idx].text = "0";
            seedTotalAmountText[idx].text = "0";
            cropTotalAmountText[idx].text = "0";
        }
    }

    private void AddButtonListeners()
    {
        for (int idx = 0; idx < 6; ++idx)
        {
            int index = idx;
            plusBtn[index].onClick.AddListener(() => OnButtonPlusClicked(index));
            minusBtn[index].onClick.AddListener(() => OnButtonMinusClicked(index));
            num1Btn[index].onClick.AddListener(() => SetAmount(index, 1));
            num5Btn[index].onClick.AddListener(() => SetAmount(index, 5));
            num10Btn[index].onClick.AddListener(() => SetAmount(index, 10));
        }
    }

    // +버튼 클릭
    void OnButtonPlusClicked(int idx)
    {
        SetButtonState(plusBtn[idx], true);
        SetButtonState(minusBtn[idx], false);
        sign[idx] = Sign.Plus;
    }

    // -버튼 클릭
    void OnButtonMinusClicked(int idx)
    {
        SetButtonState(plusBtn[idx], false);
        SetButtonState(minusBtn[idx], true);
        sign[idx] = Sign.Minus;
    }

    void SetButtonState(Button btn, bool isActive)
    {
        btn.interactable = !isActive;
    }

    // Text Update
    private void UpdateShopUI()
    {
        for (int idx = 0; idx < 3; ++idx)
        {
            seedCountText[idx].text = GameManager.instance.seedCount[idx].ToString();
            seedPriceText[idx].text = GameManager.instance.seedPrice[idx].ToString() + "원";
            seedTotalAmountText[idx].text = seedAmount[idx].ToString();
            seedTotalPriceText[idx].text = (seedAmount[idx] * GameManager.instance.seedPrice[idx]).ToString() + "원";

            cropCountText[idx].text = GameManager.instance.cropCount[idx].ToString();
            cropPriceText[idx].text = GameManager.instance.cropPrice[idx].ToString() + "원";
            cropTotalAmountText[idx].text = cropAmount[idx].ToString();
            cropTotalPriceText[idx].text = (cropAmount[idx] * GameManager.instance.cropPrice[idx]).ToString() + "원";
        }
    }


    // 구매/판매 할 총 수량 설정
    private void SetAmount(int idx, int addAmount)
    {
        if (idx < 3)
        {
            seedAmount[idx] += addAmount * (int)sign[idx];
            seedAmount[idx] = Mathf.Max(seedAmount[idx], 0);
        }
        else
        {
            int index = idx - 3;
            cropAmount[index] += addAmount * (int)sign[idx];
            cropAmount[index] = Mathf.Max(cropAmount[index], 0);
        }
    }

    public void OnClickAnimal()
    {
        CropCanvas.enabled = false;
        AnimalCanvas.enabled = true;
    }

    public void OnClickCrop()
    {
        CropCanvas.enabled = true;
        AnimalCanvas.enabled = false;
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

    public void BuyCrop(int cropIndex)
    {
        if (GameManager.money >= GameManager.instance.cropPrice[cropIndex] * cropAmount[cropIndex])
        {
            GameManager.money -= GameManager.instance.cropPrice[cropIndex] * cropAmount[cropIndex];
            GameManager.instance.cropCount[cropIndex] += cropAmount[cropIndex];
        }
        cropAmount[cropIndex] = 0;
    }

    public void SellCrop(int cropIndex)
    {
        if (GameManager.instance.cropCount[cropIndex] >= cropAmount[cropIndex])
        {
            GameManager.money += GameManager.instance.cropPrice[cropIndex] * cropAmount[cropIndex];
            GameManager.instance.cropCount[cropIndex] -= cropAmount[cropIndex];
        }
        cropAmount[cropIndex] = 0;
    }

    public void SellSeed(int seedIndex)
    {
        if (GameManager.instance.seedCount[seedIndex] >= seedAmount[seedIndex])
        {
            GameManager.money += GameManager.instance.seedPrice[seedIndex] * seedAmount[seedIndex];
            GameManager.instance.seedCount[seedIndex] -= seedAmount[seedIndex];
        }
        seedAmount[seedIndex] = 0;
    }

    public void BuySeed(int seedIndex)
    {
        if (GameManager.money >= GameManager.instance.seedPrice[seedIndex] * seedAmount[seedIndex])
        {
            GameManager.money -= GameManager.instance.seedPrice[seedIndex] * seedAmount[seedIndex];
            GameManager.instance.seedCount[seedIndex] += seedAmount[seedIndex];
        }
        seedAmount[seedIndex] = 0;
    }
}
