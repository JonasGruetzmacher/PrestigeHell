using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData gameData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataAsJson = "";
                using (FileStream streamReader = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(streamReader))
                    {
                        dataAsJson = reader.ReadToEnd();
                    }
                }

                gameData = JsonUtility.FromJson<GameData>(dataAsJson);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading game data from " + fullPath + ": " + e.Message);
            }
        }

        return gameData;
    }

    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataAsJson = JsonUtility.ToJson(gameData, true);

            using (FileStream streamWriter = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(streamWriter))
                {
                    writer.Write(dataAsJson);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving game data to " + fullPath + ": " + e.Message);
        }
    }
}
