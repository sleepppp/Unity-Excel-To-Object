using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Data.Table
{
    public class TableWriter
    {
        public static void WriteDataClassByTable(string path, Table table)
        {
            WriteData(path, null, table);
        }

        public static void WriteDataClassByTable(string path, string namesapceStr,Table table)
        {
            WriteData(path, null, table);
        }

        static void WriteData(string path, string namespaceStr,Table table)
        {
            if (table == null)
            {
                throw new System.Exception("테이블이 존재하지 않습니다.");
            }

            bool isNamespace = string.IsNullOrEmpty(namespaceStr);

            CodeGenerator generator = new CodeGenerator();

            generator.Comment("자동 데이터 테이블 생성으로 만든 데이터 입니다.");
            if (isNamespace)
            {
                generator.NameSpace(namespaceStr);
                generator.StartBlock();
            }
            generator.Class(table.name);
            generator.StartBlock();
            for (int i = 0; i < table.colCount; ++i)
            {
                generator.Tab();
                generator.Field(table.typeNames[i], table.dataNames[i]);
            }
            generator.EndBlock();
            if(isNamespace)
            {
                generator.EndBlock();
            }

            generator.WriteFile(path);
        }
    }
}