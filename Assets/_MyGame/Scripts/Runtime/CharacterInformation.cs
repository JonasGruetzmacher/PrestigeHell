using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    public class CharacterInformation : MonoBehaviour
    {
        public CharacterInformationSO CharacterInformationSO;

        public void Awake()
        {
            CharacterInformationSO.Initialize();
        }

        public void Initialize()
        {
            if (CharacterInformationSO == null)
                return;
            CharacterInformationSO.Initialize();
        }
    }
}