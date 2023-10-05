using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeroGames.Tools
{
    public interface IDataPersistence 
    {
        void LoadData(GameData gameData);
        void SaveData(GameData gameData);
    }
}