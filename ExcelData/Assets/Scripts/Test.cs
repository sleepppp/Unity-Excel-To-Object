using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using OfficeOpenXml;
using Core.Data.Table;

public class Test : MonoBehaviour
{
    void Start()
    {
        string loadPath = "Assets/Data/Test.xlsx";
        string writePath = "Assets/Data/";
        Table[] tables = TableLoader.LoadTables(loadPath);
        for(int i =0; i < tables.Length; ++i)
        {
            TableWriter.WriteDataClassByTable(writePath + tables[i].name + ".cs", tables[i]);
        }
    }
}
