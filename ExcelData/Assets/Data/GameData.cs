using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data.Utility;
using Core.Data;

namespace Core.Test
{
	public class GameData
	{
		public string SheetPath = "Assets/Data/Sheet.tsv";
		public Dictionary<int,Sheet> Sheet;

		public string Sheet2Path = "Assets/Data/Sheet2.tsv";
		public Dictionary<int,Sheet2> Sheet2;

		public GameData()
		{
			Sheet  =  TableStream.LoadTableByTSV(SheetPath).TableToDictionary<int,Sheet>();
			Sheet2  =  TableStream.LoadTableByTSV(Sheet2Path).TableToDictionary<int,Sheet2>();
		}
	}
}
