using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

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
        var temp = Time.realtimeSinceStartup;

        MMGameEvent.Trigger("Save");

        foreach (var dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(gameData);
        }
 
        fileDataHandler.Save(gameData);

        Debug.Log(string.Format("Game saved in {0} seconds", Time.realtimeSinceStartup - temp));
    }

    [Button]
    public void LoadGame()
    {
        var temp = Time.realtimeSinceStartup;

        MMGameEvent.Trigger("Load");

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

        Debug.Log(string.Format("Game loaded in {0} seconds", Time.realtimeSinceStartup - temp));
    }

    [Button]
    public void DeleteGameData()
    {
        // System.IO.File.Delete(Application.persistentDataPath + "/gameData.json");
        NewGame();
        fileDataHandler.Save(gameData);
        GetComponent<LevelSelector>().RestartLevel();
        LoadGame();
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
