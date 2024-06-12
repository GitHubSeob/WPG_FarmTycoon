using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FoodInterface : CommonInterface
{
    public override void printCounts()
    {
        Debug.Log("[Food] ChickenCount : " + chickenCount + " / DeerCount : " + deerCount + " / HorseCount : " + horseCount);
    }
}
