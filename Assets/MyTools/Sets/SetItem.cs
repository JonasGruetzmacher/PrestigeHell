using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeroGames.Tools
{
    public class SetItem<T> : MonoBehaviour
    {
        public RuntimeSet<T> Set;
        T Item;

        private void OnEnable()
        {
            Set.Add(Item);
        }

        private void OnDisable()
        {
            Set.Remove(Item);
        }
    }
}