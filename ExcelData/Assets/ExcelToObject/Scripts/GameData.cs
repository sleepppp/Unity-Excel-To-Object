using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data.Utility;

namespace Core.Data
{
	public class GameData
	{
		public string SheetPath = "Assets/Data/TSV/Sheet.tsv";
		public Dictionary<int,Sheet> Sheet;

		public string Sheet2Path = "Assets/Data/TSV/Sheet2.tsv";
		public Dictionary<int,Sheet2> Sheet2;

		public GameData()
		{
			Sheet  =  TableStream.LoadTableByTSV(SheetPath).TableToDictionary<int,Sheet>();
			Sheet2  =  TableStream.LoadTableByTSV(Sheet2Path).TableToDictionary<int,Sheet2>();
		}
	}
}
