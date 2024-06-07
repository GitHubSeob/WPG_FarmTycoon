using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{  

    // 3d object Ŭ���� �� ��ȯ
    private void OnMouseDown()
    {
        // UI�� ����� ���� ��� ����
        if (EventSystem.current.IsPointerOverGameObject()) return;
       
        if (gameObject.name == "PlantFarm") SceneManager.LoadScene("CropScene");        
        else if(gameObject.name == "Chicken") SceneManager.LoadScene("ChickenScene");
        else if (gameObject.name == "Cow") SceneManager.LoadScene("CowScene");
        else if (gameObject.name == "Sheep") SceneManager.LoadScene("SheepScene");
    }    
}
