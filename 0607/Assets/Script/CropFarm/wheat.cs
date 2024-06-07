using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class wheat : MonoBehaviour, ICrop
{

    public string cropName { get; set; }
    public int moistLevel { get; set; }
    public int fertilizerLevel { get; set; }
    public int growthLevel { get; set; }

    public bool slowMode { get; set; }
    public bool isComplete;

    public GameObject wheatA, wheatB;    

    private void Start()
    {
        cropName = "��";
        moistLevel = 60;
        growthLevel = 10;
        fertilizerLevel = 1;
        slowMode = false;
        isComplete = false;
        StartCoroutine(DecreaseMoistureLevelRoutine());
        StartCoroutine(DecreaseGrowthLevelRoutine());

        wheatA.SetActive(true);
        wheatB.SetActive(false);
    }


    private IEnumerator DecreaseMoistureLevelRoutine() // 1�ʸ���
    {
        while (true)
        {
            CheckMoist(); // ���з� üũ
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
        wheatA.SetActive(false);
        wheatB.SetActive(true);
    }

    public void OnClickHarvest()
    {
        GameManager.instance.cropCount[0] += 5 * fertilizerLevel;
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
