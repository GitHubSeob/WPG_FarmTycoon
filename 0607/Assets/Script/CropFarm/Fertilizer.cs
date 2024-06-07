using UnityEngine;

public class Fertilizer : MonoBehaviour
{
    public Camera mainCamera;
    public bool clickedFertilizer;

    void Update()
    {
        UIManager uiManager = GameObject.Find("UICanvas").GetComponent<UIManager>();
        clickedFertilizer = GameManager.instance.clickedFertilizer;
        
        if (clickedFertilizer)
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
    }
}
