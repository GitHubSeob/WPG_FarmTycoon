using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ProductionInterface : MonoBehaviour
{
    int chickenCount;
    int deerCount;
    int horseCount;

    // Start is called before the first frame update
    void Start()
    {
        chickenCount = 0;
        deerCount = 0;
        horseCount = 0;
    }

    public int getChickenCount() { return chickenCount; }
    public int getDeerCount() { return deerCount; }
    public int getHorseCount() { return horseCount; }

    public void setChickenCount(int n) { chickenCount = n; }
    public void setDeerCount(int n) { deerCount = n; }
    public void setHorseCount(int n) { horseCount = n; }

    public bool updateChickenCount(int n)
    {
        if (chickenCount + n < 0)
        {
            return false;
        }
        chickenCount += n;
        return true;
    }
    public bool updateDeerCount(int n)
    {
        if (deerCount + n < 0)
        {
            return false;
        }
        deerCount += n;
        return true;
    }
    public bool updateHorseCount(int n)
    {
        if (horseCount + n < 0)
        {
            return false;
        }
        horseCount += n;
        return true;
    }

    public void printCounts()
    {
        Debug.Log("ChickenCount : " + chickenCount + " / DeerCount : " + deerCount + " / HorseCount : " + horseCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
