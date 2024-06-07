using UnityEngine;

public class WaterPot : MonoBehaviour
{
    public Camera mainCamera;
    public bool clickedWaterPot;

    void Update()
    {
        UIManager uiManager = GameObject.Find("UICanvas").GetComponent<UIManager>();        

        if (GameManager.instance.clickedWaterPot)
        {
            FollowMouse();
        }
    }

    private void FollowMouse()
    {       
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.0f;

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        //worldPosition.z = 0;
        transform.position = worldPosition;
        GameManager.instance.isPlacementSystemActive = false;
    }

}
