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

                // Food Box
                if (selection.name.Contains("FoodBox"))
                {
                    GameObject goi = GameObject.Find("Interface");
                    goi.GetComponent<FoodInterface>().updateCount(selection.name, 3);
                    goi.GetComponent<FoodInterface>().printCounts();
                }

                // Animals
                else if (selection.tag.Contains("Chicken") || selection.tag.Contains("Deer") || selection.tag.Contains("Horse"))
                {
                    selection.GetComponent<AnimalManager>().printLog();
                }

                // Spawn New
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

                // Interface
                else if (selection.name.Contains("PITemp"))
                {
                    GameObject pi = GameObject.Find("Interface");
                    pi.GetComponent<ProductionInterface>().printCounts();
                }
                else if (selection.name.Contains("FITemp"))
                {
                    GameObject pi = GameObject.Find("Interface");
                    pi.GetComponent<FoodInterface>().printCounts();
                }

                // SlauterAll
                else if (selection.name.Contains("SlauterAll"))
                {
                    GameObject[] chickens = GameObject.FindGameObjectsWithTag("Chicken1");
                    foreach (GameObject chicken in chickens)
                    {
                        chicken.GetComponent<AnimalManager>().printLog();
                    }

                    GameObject[] horses = GameObject.FindGameObjectsWithTag("Horse1");
                    foreach (GameObject horse in horses)
                    {
                        horse.GetComponent<AnimalManager>().printLog();
                    }

                    GameObject[] deers = GameObject.FindGameObjectsWithTag("Deer1");
                    foreach (GameObject deer in deers)
                    {
                        deer.GetComponent<AnimalManager>().printLog();
                    }
                }

                // Shops
                else if (selection.name == "MainHouse"
                        || selection.name == "MeatShop"
                        || selection.name == "VegetableShop"
                        || selection.name == "Caravan"
                        || selection.name == "WeaponShop")
                {
                    Debug.Log("**SHOP** " + selection.name);
                }
            }
        }
    }
}
