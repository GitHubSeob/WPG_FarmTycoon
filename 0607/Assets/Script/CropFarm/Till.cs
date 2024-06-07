using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Till : MonoBehaviour
{
    private RaycastHit hit;
    public void OnClickHarvest()
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
