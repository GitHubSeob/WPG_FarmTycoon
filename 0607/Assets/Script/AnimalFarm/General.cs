using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class General : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject selection = hit.collider.gameObject;
                Debug.Log(selection.name);
                if (selection.name == "FoodBox")
                {
                    selection.GetComponent<FoodBox>().addFoodCount(3);
                    Debug.Log(selection.GetComponent<FoodBox>().getFoodCount());
                }
                else if (selection.name.Contains("001"))
                {
                    selection.GetComponent<ChickenManager>().printLog();
                }
            }
            else
            {
                Debug.Log("Failed");
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject fb = GameObject.Find("FoodBox");
            fb.GetComponent<FoodBox>().addFoodCount(3);
            Debug.Log(fb.GetComponent<FoodBox>().getFoodCount());
        }
    }
}
