using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data.Utility;
using Core.Data;

namespace My.Data
{
	public class GameData
	{
		public string SheetPath = "Assets/Data/TSV/Sheet.tsv";
		public Dictionary<int,Sheet> Sheet;

		public GameData()
		{
			Sheet  =  TableStream.LoadTableByTSV(SheetPath).TableToDictionary<int,Sheet>();
		}
	}
}
