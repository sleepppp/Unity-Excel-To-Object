
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Text;
using Core.Data.Code;

namespace Core.Data
{
    public static class TableStream
    {
        struct LoadInfo
        {
            public string Path;
            public string KeyTypeName;
            public string ValueTypeName;
            public string ValueName;

            public LoadInfo(string path, string keyTypeName, string valueTypeName,string valueName)
            {
                Path = path;
                KeyTypeName = keyTypeName;
                ValueTypeName = valueTypeName;
                ValueName = valueName;
            }
        }

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

        public static void WriteExcelObjectByTable(string path, Table table)
        {
            WriteExcelObject(path, null, table);
        }

        public static void WriteExcelObjectByTable(string path, string namesapceStr, Table table)
        {
            WriteExcelObject(path, namesapceStr, table);
        }

        static void WriteExcelObject(string path, string namespaceStr, Table table)
        {
            if (table == null)
            {
                throw new System.Exception("테이블이 존재하지 않습니다.");
            }

            bool isNamespace = string.IsNullOrEmpty(namespaceStr);

            CodeGenerator generator = new CodeGenerator();

            generator.Using("UnityEngine");
            generator.EndLine();
            //generator.Comment("코드 생성기를 통해 생성된 코드입니다");

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

        public static void WriteGameData(string path, string namespaceStr,Table[] tables,
            string[] tablePaths)
        {
            if (tables == null)
            {
                throw new System.Exception("테이블이 존재하지 않습니다.");
            }

            bool isNamespace = string.IsNullOrEmpty(namespaceStr) == false;

            CodeGenerator generator = new CodeGenerator();

            // {{ NameSpace ~
            generator.Using("System.Collections");
            generator.Using("System.Collections.Generic");
            generator.Using("UnityEngine");
            generator.Using("Core.Data.Utility");
            generator.Using("Core.Data");
            generator.EndLine();
            // }} 

            if(isNamespace)
            {
                generator.NameSpace(namespaceStr);
                generator.StartBlock();
            }
            //{{ InNameSpace ~
            {
                generator.Class("GameData");
                generator.StartBlock();
                // {{ InClass ~
                {
                    //generator.Singleton("GameData");

                    string stringDataName = "string";
                    string constPath = "Path";

                    List<LoadInfo> loadInfo = new List<LoadInfo>();

                    for (int i = 0; i < tables.Length; ++i)
                    {
                        LoadInfo info = new LoadInfo(tablePaths[i], tables[i].typeNames[0],
                            tables[i].name, tables[i].name);
                        loadInfo.Add(info);

                        generator.StringField(stringDataName, tables[i].name + constPath, tablePaths[i]);

                        generator.Dictionary(tables[i].typeNames[0], tables[i].name, tables[i].name);

                        generator.EndLine();
                    }

                    generator.StartConstructor("GameData");
                    {
                        for (int i = 0; i < loadInfo.Count; ++i)
                        {
                            generator.CheckTab();
                            generator.Append(loadInfo[i].ValueName).Space().Append(" = ").Space()
                                .Append("TableStream.LoadTableByTSV(").Append(tables[i].name + constPath)
                                .Append(")").Append(".TableToDictionary<").Append(loadInfo[i].KeyTypeName)
                                .Append(",").Append(loadInfo[i].ValueTypeName).Append(">();").EndLine();
                        }
                    }
                    generator.EndConstructor();
                }
                // ~EndClass }}
                generator.EndBlock();
            }
            // ~EndNameSpace }}
            if(isNamespace)
            {
                generator.EndBlock();
            }

            generator.WriteFile(path);
        }
    }
}