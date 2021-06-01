using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using My.Data;

public class Demo : MonoBehaviour
{
    void Start()
    {
        GameData data = new GameData();
        Dictionary<int, Sheet> sheetData = data.Sheet;
    }
}
