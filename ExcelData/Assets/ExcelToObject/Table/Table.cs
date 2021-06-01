using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace Core.Data
{
    public class Table
    {
        int m_rowCount;
        int m_colCount;

        string m_name;
        string[] m_typeNames;
        string[] m_dataNames;
        string[,] m_data;

        public string name { get { return m_name; } }
        public int rowCount { get { return m_rowCount; } }
        public int colCount { get { return m_colCount; } }
        public string[] typeNames { get { return m_typeNames; } }
        public string[] dataNames { get { return m_dataNames; } }
        public string[,] data { get { return m_data; } }

        public static Table Create(ExcelWorksheet sheet)
        {
            Table result = null;

            if (sheet == null)
                return result;

            ExcelCellAddress start = sheet.Dimension.Start;
            ExcelCellAddress end = sheet.Dimension.End;

            int colCount = end.Column - start.Column + 1;
            int rowCount = end.Row - start.Row + 1;

            string[] titles;
            string[,] data;

            titles = new string[colCount];
            data = new string[rowCount - 1, colCount];

            for(int y = start.Row, indexY = 0; y <= end.Row; ++y , ++indexY)
            {
                for(int x= start.Column,indexX = 0 ; x <= end.Column; ++x, ++indexX)
                {
                    ExcelRange range = sheet.Cells[y, x];
                    if (y == start.Row)
                    {
                        titles[indexX] = range.Text;
                    }
                    else
                    {
                        data[indexY - 1, indexX] = range.Text;
                    }
                }
            }

            result = new Table(sheet.Name,rowCount - 1, colCount, titles, data);

            return result;
        }

        public static Table Create(string fileName,string tsvText)
        {
            Table result = null;
            string lineSplitToken = @"\r\n|\n\r|\n|\r";
            string[] lines = Regex.Split(tsvText, lineSplitToken);
            if (lines == null || lines.Length <= 1)
                return result;
        
            string tabToken = @"\t";
            string[] dataNames = Regex.Split(lines[0], tabToken);
        
            int rowCount = lines.Length - 1;
            int colCount = dataNames.Length;
        
            string[,] data = new string[rowCount, colCount];
            for(int y =0;y < rowCount; ++y)
            {
                string[] line = Regex.Split(lines[y + 1], tabToken);

                for(int x=0; x < colCount; ++x)
                {
                    data[y,x] = line[x];
                }
            }

            return new Table(fileName, rowCount, colCount, dataNames, data);
        }

        public Table(string tableName,int rowCount, int colCount,string[] titles, string[,] data)
        {
            m_name = tableName;
            m_rowCount = rowCount;
            m_colCount = colCount;
            m_data = data;

            m_typeNames = new string[colCount];
            m_dataNames = new string[colCount];

            for(int i =0; i < titles.Length; ++i)
            {
                TableUtility.SplitDataNameAndTypeName(titles[i],out m_dataNames[i],out m_typeNames[i]);
            }

            //for(int y=0;y < m_rowCount; ++y)
            //{
            //    for(int x=0;x < m_colCount; ++x)
            //    {
            //        m_data[y, x] = TableUtility.RemoveSmallBracket(m_data[y,x]);
            //    }
            //}
        }
    }
}