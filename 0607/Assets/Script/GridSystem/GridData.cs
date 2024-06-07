using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex);
        foreach(var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                //throw new Exception($"Dictionary already contains this cell position {pos}");
            }
            placedObjects[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }
        return returnVal;
    }


    // 같은 오브젝트에 대해서 겹치는지 확인 지을 수 있으면 True
    public bool CanPlacedObjectAt(Vector3Int gridPosition, int selectedObjectIndex)
    {
        Vector3Int AgridPosition = gridPosition;
        AgridPosition.z = gridPosition.z * -1 + 4;
        AgridPosition.x = gridPosition.x + 5;
        int prevIndex = GameManager.instance.objectArray[AgridPosition.z, AgridPosition.x];

        
        if (selectedObjectIndex > 0 && GameManager.instance.seedCount[selectedObjectIndex-1] == 0)
        {
            return false;
        }

        // 현재 선택된 object가 씨앗일때, 밭이 개간되었는지를 판단
        if (selectedObjectIndex > 0 && prevIndex == 0)
        {
            return true;
        }
        else if (selectedObjectIndex == 0 && prevIndex == -1)
        {            
            return true;
        }        
        return false;
    }
}

public class PlacementData
{   
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int id, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = id;
        PlacedObjectIndex = placedObjectIndex;
    }
}
