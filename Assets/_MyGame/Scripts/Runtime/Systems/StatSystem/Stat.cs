using System;
using System.Collections;
using System.Collections.Generic;
using LeroGames.Tools;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;


namespace LeroGames.PrestigeHell
{
    [CreateAssetMenu(fileName = "Stat", menuName = "PrestigeHell/Stats")]
    public class Stat : ScriptableObject
    {
        public StatType StatType;
        public FloatVariable BaseValue;
        public FloatVariable UpgradedValue;

        public void Reset()
        {
            Initialize();
        }

        [Button]
        public void CreateStatAsset( float baseValue = 0f, string name = "Test")
        {

            BaseValue = CreateInstance<FloatVariable>();
            BaseValue.name = String.Format("Base{0}{1}", name, StatType.ToString());
            BaseValue.Value = baseValue;
            if (!Directory.Exists(string.Format("Assets/_MyGame/SO/Stats/BaseStats/{0}", name)))
            {
                Directory.CreateDirectory(string.Format("Assets/_MyGame/SO/Stats/BaseStats/{0}", name));
            }
            AssetDatabase.CreateAsset(BaseValue, String.Format("Assets/_MyGame/SO/Stats/BaseStats/{0}/{1}.asset", name, BaseValue.name));
            

            UpgradedValue = CreateInstance<FloatVariable>();
            UpgradedValue.name = String.Format("Upgraded{0}{1}", name, StatType.ToString());
            UpgradedValue.Value = baseValue;
            if (!Directory.Exists(string.Format("Assets/_MyGame/SO/Stats/UpgradedStats/{0}", name)))
            {
                Directory.CreateDirectory(string.Format("Assets/_MyGame/SO/Stats/UpgradedStats/{0}", name));
            }
            AssetDatabase.CreateAsset(UpgradedValue, String.Format("Assets/_MyGame/SO/Stats/UpgradedStats/{0}/{1}.asset",name, UpgradedValue.name));
            Debug.Log(string.Format("Created {0} stat for {1}", StatType, name));

            AssetDatabase.SaveAssets();
        }

        public void OnValidate()
        {
            Initialize();
        }

        [Button]
        public void Initialize()
        {
            if (BaseValue == null) 
            {
                TryFindBaseValue();
                return;
            }
            if (UpgradedValue == null)
            {
                TryFindUpgradedValue();
                return;
            }
                
            UpgradedValue?.SetValue(BaseValue);
        }

        public void OnEnable()
        {
            Initialize();
        }

        public void OnReset()
        {
            Initialize();
        }

        private void TryFindBaseValue()
        {
            BaseValue = (FloatVariable)AssetDatabase.LoadAssetAtPath<FloatVariable>(string.Format("Assets/_MyGame/SO/Stats/BaseStats/{0}/Base{1}.asset", name.Replace(StatType.ToString(), ""), name, typeof(FloatVariable)));
        }

        private void TryFindUpgradedValue()
        {
            UpgradedValue = (FloatVariable)AssetDatabase.LoadAssetAtPath<FloatVariable>(string.Format("Assets/_MyGame/SO/Stats/UpgradedStats/{0}/Upgraded{1}.asset", name.Replace(StatType.ToString(), ""), name, typeof(FloatVariable)));
        }
    }
}