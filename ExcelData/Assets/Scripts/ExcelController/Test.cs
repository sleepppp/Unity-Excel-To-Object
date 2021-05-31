using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;


public class Test : MonoBehaviour
{
    void Start()
    {
        string loadPath = "Assets/Data/Test.xlsx";
        string writePath = "Assets/Data/";
        Table[] tables = TableStream.LoadTablesByXLSX(loadPath);
        for(int i =0; i < tables.Length; ++i)
        {
            TableStream.WriteCodeByTable(writePath + tables[i].name + ".cs","Core.Data", tables[i]);
            TableStream.WriteTSVByTable( writePath + tables[i].name + ".tsv", tables[i]);
        }

        Table loadTSV = TableStream.LoadTableByTSV("Assets/Data/TestSheet.tsv");
        Debug.Log("End Test");
    }
}
