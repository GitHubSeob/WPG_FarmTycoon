using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayermask;

    public event Action OnClicked, OnExit;



    private void Update()        
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.isPlacementSystemActive = false;
            Escape();
        }

        if (GameManager.instance.clickedWaterPot || GameManager.instance.clickedFertilizer)
        {            
            GameManager.instance.isPlacementSystemActive = false;
            PlacementSystem.instance.StopPlacement();
        }
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
    
    public void Escape()
    {
        OnExit?.Invoke();
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 15, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
