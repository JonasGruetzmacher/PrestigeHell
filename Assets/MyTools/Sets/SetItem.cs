using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeroGames.Tools
{
    public class SetItem : MonoBehaviour
    {
        public RuntimeSet Set;

        private void OnEnable()
        {
            Set.Add(gameObject);
        }

        private void OnDisable()
        {
            Set.Remove(gameObject);
        }
    }
}