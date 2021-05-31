using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Text;
using Core.Data.Code;
namespace Core.Data
{
    public static class TableStream
    {
        public static Table[] LoadTablesByXLSX(string path)
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

        public static Table LoadTableByTSV(string path)
        {
            string splitFileName = @"/";
            string[] arrSplit = Regex.Split(path, splitFileName);
            string fileName = arrSplit[arrSplit.Length - 1];
            
            using (FileStream stream = File.Open(path,FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                string body = reader.ReadToEnd();
                return Table.Create(fileName, body);
            }
        }

        public static void WriteTSVByTable(string writePath,Table table)
        {
            using (StreamWriter stream = new StreamWriter(writePath) )
            {
                const string tabToken = "\t";
                const string openBracket = "(";
                const string closeBracket = ")";

                StringBuilder builder = new StringBuilder();
                for(int i =0; i < table.dataNames.Length; ++i)
                {
                    builder.Append(table.dataNames[i]);
                    builder.Append(openBracket);
                    builder.Append(table.typeNames[i]);
                    builder.Append(closeBracket);
                    if (i != table.dataNames.Length - 1)
                        builder.Append(tabToken);
                }

                for(int y=0; y < table.rowCount; ++y)
                {
                    builder.AppendLine();
                    for (int x = 0;x < table.colCount; ++x)
                    {
                        builder.Append(table.data[y, x]);
                        if(x != table.colCount - 1)
                            builder.Append(tabToken);
                    }
                }

                stream.Write(builder.ToString());
            }
        }

        public static void WriteCodeByTable(string path, Table table)
        {
            WriteCode(path, null, table);
        }

        public static void WriteCodeByTable(string path, string namesapceStr, Table table)
        {
            WriteCode(path, namesapceStr, table);
        }

        static void WriteCode(string path, string namespaceStr, Table table)
        {
            if (table == null)
            {
                throw new System.Exception("테이블이 존재하지 않습니다.");
            }

            bool isNamespace = string.IsNullOrEmpty(namespaceStr);

            CodeGenerator generator = new CodeGenerator();

            generator.Using("UnityEngine");
            generator.EndLine();
            generator.Comment("코드 생성기를 통해 생성된 코드입니다");

            if (isNamespace == false)
            {
                generator.NameSpace(namespaceStr);
                generator.StartBlock();
            }
            generator.Class(table.name);
            generator.StartBlock();
            for (int i = 0; i < table.colCount; ++i)
            {
                generator.Field(table.typeNames[i], table.dataNames[i]);
            }
            generator.EndBlock();
            if (isNamespace == false)
            {
                generator.EndBlock();
            }

            generator.WriteFile(path);
        }
    }
}