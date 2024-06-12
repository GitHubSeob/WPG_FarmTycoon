using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ProductionInterface : CommonInterface
{
    public override void printCounts()
    {
        Debug.Log("[Product] ChickenCount : " + chickenCount + " / DeerCount : " + deerCount + " / HorseCount : " + horseCount);
    }
}
