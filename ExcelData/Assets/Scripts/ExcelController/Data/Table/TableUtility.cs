using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.Data
{
    public static class TableUtility
    {
        public const string c_int = @"int|Int|INT";

        public static void SplitDataNameAndTypeName(string origin, out string dataName, out string typeName)
        {
            int startBracket = origin.IndexOf('(');
            int endBracket = origin.IndexOf(')');

            if (startBracket == -1 || endBracket == -1)
            {
                throw new System.Exception("데이터 타입이 명시되어 있지 않습니다");
            }

            dataName = origin.Substring(0, startBracket);
            typeName = origin.Substring(startBracket + 1, endBracket - startBracket - 1);
        }

        public static string RemoveSmallBracket(string origin)
        {
            int startBracket = origin.IndexOf('(');
            int endBracket = origin.IndexOf(')');
            if (startBracket == -1 || endBracket == -1)
                return origin;
            return origin.Substring(startBracket + 1, endBracket - startBracket - 1);
        }

        //TODO 데이터 타입에 오탈자 검사 해서 보정
        //public static string ReviseTypeName(string origin)
        //{
        //    if(Regex.IsMatch())
        //    return null;
        //}
    }
}
