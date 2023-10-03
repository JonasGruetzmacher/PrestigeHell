using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager instance { get; private set; }


    [SerializeField] private string fileName = "gameData.json";

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler fileDataHandler;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogError("Multiple instances of DataPersistenceManager detected. Destroying duplicate.");
        }
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    [Button]
    public void SaveGame()
    {
        foreach (var dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(gameData);
        }
 
        fileDataHandler.Save(gameData);
    }

    [Button]
    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No game data found. Creating new game data.");
            NewGame();
        }
        
        foreach (var dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(gameData);
        }
    }

    [Button]
    public void DeleteGameData()
    {
        System.IO.File.Delete(Application.persistentDataPath + "/gameData.json");
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
        return dataPersistenceObjects.ToList();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
