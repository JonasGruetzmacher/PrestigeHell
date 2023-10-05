using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace LeroGames.Tools
{
    [System.Serializable]
    public class GameData
    {
        public float xp;
        public SerializableDictionary<string, float> statistics = new SerializableDictionary<string, float>();
        public SerializableDictionary<string, int> permanentUpgrades = new SerializableDictionary<string, int>();

        public GameData()
        {
            this.xp = 0;
            this.statistics = new SerializableDictionary<string, float>();
            this.permanentUpgrades = new SerializableDictionary<string, int>();

        }
    }
}