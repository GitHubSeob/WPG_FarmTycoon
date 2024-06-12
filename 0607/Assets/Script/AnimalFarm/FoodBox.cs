using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBox : MonoBehaviour
{
    int foodCount;

    public int getFoodCount()
    {
        return foodCount;
    }

    public void setFoodCount(int n)
    {
        foodCount = n;
    }

    public bool decFoodCount()
    {
        if (foodCount > 0)
        {
            --foodCount;
            return true;
        }
        return false;
    }

    public void addFoodCount(int n)
    {
        foodCount += n;
    }

    // Start is called before the first frame update
    void Start()
    {
        foodCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
