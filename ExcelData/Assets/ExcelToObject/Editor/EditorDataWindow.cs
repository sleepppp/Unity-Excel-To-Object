
using UnityEngine;
using UnityEditor;
using System.IO;

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

            if(GUILayout.Button("Create Data Files"))
            {
                CreateDataFiles();
            }
        }

        void CreateDataFiles()
        {
            string excelFolderPath = AssetDatabase.GetAssetPath(m_loadData.ExcelFolder);
            string codeFolderPath = AssetDatabase.GetAssetPath(m_loadData.CodeFolder);
            string tsvFolderPath = AssetDatabase.GetAssetPath(m_loadData.TSVFolder);

            DirectoryInfo directoryInfo = new DirectoryInfo(excelFolderPath);
            string xlsx = ".xlsx";
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                //같으면 0을 반환함
                if (fileInfo.Extension.ToLower().CompareTo(xlsx) == 0)
                {
                    Table[] tables = TableStream.LoadTablesByXLSX(fileInfo.FullName);
                    string[] tsvPathes = new string[tables.Length];
                    for (int i = 0; i < tables.Length; ++i)
                    {
                        TableStream.WriteExcelObjectByTable(codeFolderPath + "/" + tables[i].name + ".cs",
                            m_loadData.NameSpace, tables[i]);
                        tsvPathes[i] = tsvFolderPath + "/" + tables[i].name + ".tsv";
                        TableStream.WriteTSVByTable(tsvPathes[i], tables[i]);
                    }

                    //TODO 추후에 코드 생성기 개선되면 자동으로 GameData클래스 생성되게 구현
                    TableStream.WriteGameData(codeFolderPath + "/GameData.cs", m_loadData.NameSpace, tables,
                        tsvPathes);
                }
            }

            AssetDatabase.Refresh();
        }
    }
}
