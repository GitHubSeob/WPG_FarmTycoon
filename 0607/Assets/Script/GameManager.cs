using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public static int money = 0;

    public int[] seedCount = new int[3];
    public int[] seedPrice = new int[3];
    public int[] cropCount = new int[3];
    public int[] cropPrice = new int[3];
    public int[,] objectArray = new int[10, 10];

    public Canvas[] canvases;

    public bool isPlacementSystemActive;
    private static bool isSceneFirstLoaded = true;
    public bool clickedWaterPot = false;
    public bool clickedFertilizer = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (isSceneFirstLoaded)
            {
                InitializeGame();
                isSceneFirstLoaded = false;
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    // 게임 시작시 초기화 값
    void InitializeGame()
    {
        money = 1000;
        seedCount[0] = 10;
        seedPrice[0] = 50;
        seedPrice[1] = 60;
        seedPrice[2] = 70;
        cropPrice[0] = 100;
        cropPrice[1] = 200;
        cropPrice[2] = 300;

        for (int z = 0; z < 10; ++z)
        {
            for (int x = 0; x < 10; ++x)
            {
                objectArray[z, x] = -1;
            }
        }

        isPlacementSystemActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach(Canvas canvas in canvases)
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }

    public void DisableOtherMousePointer()
    {
        clickedWaterPot = clickedFertilizer = false;
    }
}   
