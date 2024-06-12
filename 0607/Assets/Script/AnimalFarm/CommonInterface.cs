using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonInterface : MonoBehaviour
{
    protected int chickenCount;
    protected int horseCount;
    protected int deerCount;

    // Start is called before the first frame update
    void Start()
    {
        chickenCount = 0;
        horseCount = 0;
        deerCount = 0;
    }

    public int getChickenCount() { return chickenCount; }
    public int getHorseCount() { return horseCount; }
    public int getDeerCount() { return deerCount; }
    public int getCount(string target)
    {
        if (target.Contains("Chicken"))
            return chickenCount;
        else if (target.Contains("Horse"))
            return horseCount;
        else if (target.Contains("Deer"))
            return deerCount;
        else
            return -1;
    }

    public void setChickenCount(int n) { chickenCount = n; }
    public void setHorseCount(int n) { horseCount = n; }
    public void setDeerCount(int n) { deerCount = n; }
    public void setCount(string target, int n)
    {
        if (target.Contains("Chicken"))
        {
            chickenCount = n;
        }
        else if (target.Contains("Horse"))
        {
            horseCount = n;
        }
        else if (target.Contains("Deer"))
        {
            deerCount = n;
        }
    }

    public bool updateChickenCount(int n)
    {
        if (chickenCount + n < 0)
        {
            return false;
        }
        chickenCount += n;
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
    public bool updateDeerCount(int n)
    {
        if (deerCount + n < 0)
        {
            return false;
        }
        deerCount += n;
        return true;
    }
    public bool updateCount(string target, int n)
    {
        if (target.Contains("Chicken"))
        {
            return updateChickenCount(n);
        }
        else if (target.Contains("Horse"))
        {
            return updateHorseCount(n);
        }
        else if (target.Contains("Deer"))
        {
            return updateDeerCount(n);
        }
        else
            return false;
    }

    public abstract void printCounts();
}
