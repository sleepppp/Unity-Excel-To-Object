using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;
using Core.Data.Utility;
using System.Reflection;
using System;

public class Test : MonoBehaviour
{
    GameData m_data;

    void Start()
    {
        m_data = new GameData();
        Sheet sheet;
        m_data.Sheet.TryGetValue(1, out sheet);
    }
}
