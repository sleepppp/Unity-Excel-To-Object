using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Core.Data
{
    public class DataScriptableObject : ScriptableObject
    {
        public UnityEngine.Object ExcelFolder;
        public UnityEngine.Object CodeFolder;
        public UnityEngine.Object TSVFolder;

        [MenuItem("Assets/Create/Data/LoadData")]
        static void Create()
        {
            DataScriptableObject instance = CreateInstance<DataScriptableObject>();

            if (AssetDatabase.IsValidFolder("Assets/Data") == false)
            {
                AssetDatabase.CreateFolder("Assets", "Data");
            }

            AssetDatabase.CreateAsset(instance, "Assets/Data/LoadData.asset");
        }
    }
}