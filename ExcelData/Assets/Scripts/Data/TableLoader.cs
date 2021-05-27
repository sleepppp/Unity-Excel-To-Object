using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OfficeOpenXml;

namespace Core.Data.Table
{
    public class TableLoader : MonoBehaviour
    {
        public static Table[] LoadTables(string path)
        {
            Table[] result = null;

            byte[] bin = File.ReadAllBytes(path);
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPakage = new ExcelPackage(stream))
            {
                ExcelWorkbook workBook = excelPakage.Workbook;
                result = new Table[workBook.Worksheets.Count];
                int index = 0;
                foreach (ExcelWorksheet sheet in workBook.Worksheets)
                {
                    result[index] = Table.Create(sheet);
                    ++index;
                }
            }

            return result;
        }
    }
}