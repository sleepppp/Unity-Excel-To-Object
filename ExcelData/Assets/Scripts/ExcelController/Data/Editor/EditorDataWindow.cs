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

        private void OnGUI()
        {
            
        }
    }
}
