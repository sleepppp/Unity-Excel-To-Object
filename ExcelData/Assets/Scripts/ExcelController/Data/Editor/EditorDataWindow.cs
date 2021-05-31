using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Core.Data
{
    public class EditorDataWindow : EditorWindow
    {
        [MenuItem("Window/Data/ExcelData")]
        static void Open()
        {
            EditorDataWindow window = 
                EditorWindow.GetWindow(typeof(EditorDataWindow)) as EditorDataWindow;
            if (window == null)
                return;

            window.Show();
        }

        DataScriptableObject m_loadData;

        private void OnGUI()
        {
            m_loadData = EditorGUILayout.ObjectField("Load Data", m_loadData,
                typeof(DataScriptableObject), false, null) as DataScriptableObject;

            if(GUILayout.Button("Created Data Files"))
            {
                
            }
        }
    }
}
