
using UnityEngine;

namespace Core.Data.Utility
{
    public static class VectorConverter
    {
        public static Vector3 ConvertToVector3(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Vector3.zero;

            Vector3 result = new Vector3();
            string[] arrStr = TableUtility.Split(value, ",");
            result.x = float.Parse(arrStr[0]);
            if (arrStr.Length >= 2)
                result.y = float.Parse(arrStr[1]);
            if (arrStr.Length >= 3)
                result.z = float.Parse(arrStr[2]);

            return result;
        }

        public static Vector2 ConvertToVector2(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Vector2.zero;

            Vector2 result = new Vector2();
            string[] arrStr = TableUtility.Split(value, ",");
            result.x = float.Parse(arrStr[0]);
            if (arrStr.Length >= 2)
                result.y = float.Parse(arrStr[1]);

            return result;
        }
    }

}