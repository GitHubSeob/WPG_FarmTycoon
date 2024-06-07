using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrop
{
    string cropName { get; set; }
    int moistLevel { get; set; }
    int fertilizerLevel { get; set; }
    int growthLevel { get; set; }

    bool slowMode { get; set; }

    void OnClickHarvest();
}