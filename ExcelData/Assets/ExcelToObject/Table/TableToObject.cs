using System.Collections.Generic;
using Core.Data;
using System;
using System.Reflection;
using UnityEngine;

namespace Core.Data.Utility
{
    public static class TableToObject
    {
        static readonly Type _vector3Type = typeof(Vector3);
        static readonly Type _vector2Type = typeof(Vector2);

        public static Dictionary<TKey,TValue> TableToDictionary<TKey,TValue>(this Table table)
            where TValue : class,new()
        {
            if (table == null)
                return null;

            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

            List<TValue> list = TableToList<TValue>(table);

            Type type = typeof(TValue);
            FieldInfo[] fields = type.GetFields();
            
            for(int i =0; i < list.Count;++i)
            {
                object key = fields[0].GetValue(list[i]);
                result.Add((TKey)key, list[i]);
            }

            return result;
        }

        public static List<T> TableToList<T>(this Table table) where T : class,new()
        {
            if (table == null)
                return null;

            List<T> result = new List<T>(table.rowCount);

            System.Type type = typeof(T);
            System.Reflection.FieldInfo[] arrayFieldInfo = type.GetFields();

            for (int y = 0; y < table.rowCount; ++y)
            {
                T newData = new T();
                for (int x = 0; x < arrayFieldInfo.Length; ++x)
                {
                    System.Type fieldType = arrayFieldInfo[x].FieldType;

                    // {{ 배열,Vector3,Vector2 예외 처리
                    if(fieldType.IsArray)
                    {
                        SetArrayValue(newData, arrayFieldInfo[x], table.data[y, x]);
                    }
                    // {{ TODO TypeConverter활용 해서 개선 필요~
                    else if(fieldType == _vector3Type)
                    {
                        arrayFieldInfo[x].SetValue(newData, VectorConverter.ConvertToVector3(table.data[y, x]));
                    }
                    else if(fieldType ==_vector2Type)
                    {
                        arrayFieldInfo[x].SetValue(newData, VectorConverter.ConvertToVector2(table.data[y, x]));
                    }
                    // }}
                    // }}
                    else
                    {
                        arrayFieldInfo[x].
                            SetValue(newData, TypeConverter.ConvertType(table.data[y, x], fieldType));
                    }
                }
                result.Add(newData);
            }

            return result;
        }

        static void SetArrayValue(object instance,FieldInfo fieldInfo,string value)
        {
            System.Type fieldType = fieldInfo.FieldType;

            Type elementType = fieldType.GetElementType();
            string[] arrStr = TableUtility.Split(value, ",");
            Array arrValue = Array.CreateInstance(elementType, arrStr.Length);
            for (int i = 0; i < arrValue.Length; ++i)
            {
                object objectValue = TypeConverter.ConvertType(arrStr[i], elementType);
                arrValue.SetValue(objectValue, i);
            }

            fieldInfo.SetValue(instance, arrValue);
        }
    }
}