using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class General : MonoBehaviour
{
    public Camera farmView;
    public GameObject chicken;
    public GameObject deer;
    public GameObject horse;

    public int chickenCount;
    public int deerCount;
    public int horseCount;

    [SerializeField]
    private int chickenMaxCount;
    [SerializeField]
    private int deerMaxCount;
    [SerializeField]
    private int horseMaxCount;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        chickenCount = 0;
        deerCount = 0;
        horseCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = farmView.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject selection = hit.collider.gameObject;
                Debug.Log("Ray : " + selection.name);
                if (selection.name == "HorseFoodBox")
                {
                    selection.GetComponent<FoodBox>().addFoodCount(3);
                    Debug.Log("HorseFoodBox Count : " + selection.GetComponent<FoodBox>().getFoodCount());
                }
                else if (selection.name == "DeerFoodBox")
                {
                    selection.GetComponent<FoodBox>().addFoodCount(3);
                    Debug.Log("DeerFoodBox Count : " + selection.GetComponent<FoodBox>().getFoodCount());
                }
                if (selection.name == "ChickenFoodBox")
                {
                    selection.GetComponent<FoodBox>().addFoodCount(3);
                    Debug.Log("ChickenFoodBox Count : " + selection.GetComponent<FoodBox>().getFoodCount());
                }
                else if (selection.name.Contains("Chicken_") || selection.name.Contains("Deer_") || selection.name.Contains("Horse_"))
                {
                    selection.GetComponent<AnimalManager>().printLog();
                }
                else if (selection.name.Contains("NewDeer"))
                {
                    if (deerCount < deerMaxCount)
                    {
                        GameObject spawn = GameObject.Find("DeerSpawn");
                        Instantiate(deer, spawn.transform.position, spawn.transform.rotation);
                        ++deerCount;
                    }
                }
                else if (selection.name.Contains("NewHorse"))
                {
                    if (horseCount < horseMaxCount)
                    {
                        GameObject spawn = GameObject.Find("HorseSpawn");
                        Instantiate(horse, spawn.transform.position, spawn.transform.rotation);
                        ++horseCount;
                    }
                }
                else if (selection.name.Contains("NewChicken"))
                {
                    if (chickenCount < chickenMaxCount)
                    {
                        GameObject spawn = GameObject.Find("ChickenSpawn");
                        Instantiate(chicken, spawn.transform.position, spawn.transform.rotation);
                        ++chickenCount;
                    }
                }
                else if (selection.name.Contains("PITemp"))
                {
                    GameObject pi = GameObject.Find("ProductionInterface");
                    pi.GetComponent<ProductionInterface>().printCounts();
                }
            }
            else
            {
                Debug.Log("Ray Failed");
            }
        }
    }
}
