using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{  

    // 3d object 클릭시 씬 전환
    private void OnMouseDown()
    {
        // UI가 띄어져 있을 경우 무시
        if (EventSystem.current.IsPointerOverGameObject()) return;
       
        if (gameObject.name == "PlantFarm") SceneManager.LoadScene("CropScene");        
        else if(gameObject.name == "Chicken") SceneManager.LoadScene("ChickenScene");
        else if (gameObject.name == "Cow") SceneManager.LoadScene("CowScene");
        else if (gameObject.name == "Sheep") SceneManager.LoadScene("SheepScene");
    }    
}
