using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    public static PlacementSystem instance;

    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualization;

    private GridData gridData;
    private List<GameObject> placedGameObjects = new();

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;


    public AudioClip tillSound;
    private AudioSource musicPlayer;

    private void Start()
    {       
        StopPlacement();
        gridData = new();
        musicPlayer = gameObject.AddComponent<AudioSource>();
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        GameManager.instance.isPlacementSystemActive = true;
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisualization.SetActive(true);
        preview.StartShowingPlacementPreview(database.objectsData[selectedObjectIndex].Prefab, database.objectsData[selectedObjectIndex].Size);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;        
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }        

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        Vector3Int AgridPosition = gridPosition;
        AgridPosition.z = gridPosition.z * -1 + 4;
        AgridPosition.x = gridPosition.x + 5;


        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false) return;    


        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);


        // 새 인스턴스 이름 부여: 오브젝트명[인덱스].[생성번째]
        string objectName = database.objectsData[selectedObjectIndex].Name;
        newObject.name = $"{objectName}_{AgridPosition.z}{AgridPosition.x}";

        PlaySound();
        placedGameObjects.Add(newObject);
        GridData selectedData = gridData;
        selectedData.AddObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID, placedGameObjects.Count - 1);
        preview.UpdatePosition(grid.CellToWorld(gridPosition), false);

        GameManager.instance.objectArray[AgridPosition.z, AgridPosition.x] = selectedObjectIndex;
        if (selectedObjectIndex > 0)
            GameManager.instance.seedCount[selectedObjectIndex - 1]--;
    }

    public void PlaySound()
    {
        musicPlayer.clip = tillSound;
        musicPlayer.Play();
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = gridData;

        return selectedData.CanPlacedObjectAt(gridPosition, selectedObjectIndex);
    }

    public void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        preview.StopShowingPreview();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
    }

    private void Update()
    {
        if (selectedObjectIndex < 0) return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPosition != gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

            mouseIndicator.transform.position = mousePosition;
            preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
            lastDetectedPosition = gridPosition;
        }
    }
}
