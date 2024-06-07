using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class corn : MonoBehaviour, ICrop
{

    public string cropName { get; set; }
    public int moistLevel { get; set; }
    public int fertilizerLevel { get; set; }
    public int growthLevel { get; set; }

    public bool slowMode { get; set; }
    public bool isComplete;

    public GameObject cornA, cornB;

    private void Start()
    {
        cropName = "옥수수";
        moistLevel = 60;
        growthLevel = 10;
        fertilizerLevel = 1;
        slowMode = false;
        isComplete = false;
        StartCoroutine(DecreaseMoistureLevelRoutine());
        StartCoroutine(DecreaseGrowthLevelRoutine());

        cornA.SetActive(true);
        cornB.SetActive(false);
    }


    private IEnumerator DecreaseMoistureLevelRoutine() // 1초마다
    {
        while (true)
        {
            CheckMoist(); // 수분량 체크
            yield return new WaitForSeconds(1);
            moistLevel--;
        }
    }

    private IEnumerator DecreaseGrowthLevelRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(slowMode ? 2 : 1);
            --growthLevel;

            if (growthLevel == 0)
            {
                OnGrowthComplete();
            }
        }
    }

    public void CheckMoist()
    {
        slowMode = moistLevel < 50;
    }

    void OnGrowthComplete()
    {
        isComplete = true;

        cornA.SetActive(false);
        cornB.SetActive(true);
    }

    public void OnClickHarvest()
    {
        GameManager.instance.cropCount[2] += 5 * fertilizerLevel;
        string objectName = this.transform.gameObject.name;
        int objectLen = objectName.Length;
        int objectZ = objectName[objectLen - 2] - '0';
        int objectX = objectName[objectLen - 1] - '0';
        GameManager.instance.objectArray[objectZ, objectX] = -1;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);

        GameObject till = GameObject.Find($"till_{objectZ}{objectX}");
        till.SetActive(false);
        Destroy(till);
    }
}
