using System;
using UnityEngine;
using VRC.SDK3.Data;
using UdonSharp;
using Object = UnityEngine.Object;

namespace Koyashiro.UdonTest
{
    public static class Assert
    {
        private const string COLOR_TAG = "cyan";
        private const string CULOR_OK = "lime";
        private const string COLOR_FAILED = "red";
        private const string COLOR_EXPECTED = "lime";
        private const string COLOR_ACTUAL = "red";

        public static void Equal(object expected, object actual, Object context = null)
        {
            if (!Equals(expected, actual))
            {
                LogFailed(expected, actual, context);
                return;
            }

            LogOk(expected, actual, context);
        }

        public static void True(bool actual, Object context = null)
        {
            if (!actual)
            {
                LogFailed(true, actual, context);
                return;
            }

            LogOk(true, actual, context);
        }

        public static void False(bool actual, Object context = null)
        {
            if (actual)
            {
                LogFailed(false, actual, context);
                return;
            }

            LogOk(false, actual, context);
        }

        public static void Null(object actual, Object context = null)
        {
            if (actual != null)
            {
                LogFailed(null, actual, context);
                return;
            }

            LogOk(null, actual, context);
        }

        private static void LogOk(object expected, object actual, Object context)
        {
            var expectedType = expected == null ? "null" : expected.GetType().ToString();
            var actualType = actual == null ? "null" : actual.GetType().ToString();
            var message = string.Concat(
                $"[<color={COLOR_TAG}>UdonTest</color>] Test <color={CULOR_OK}>OK!</color>\n",
                $"expected: <color={COLOR_EXPECTED}>{ToDebugString(expected)}</color> ({expectedType})\t",
                $"actual: <color={COLOR_ACTUAL}>{ToDebugString(actual)}</color> ({actualType})");
            Debug.Log(message, context);
        }

        private static void LogFailed(object expected, object actual, Object context)
        {
            var expectedType = expected == null ? "null" : expected.GetType().ToString();
            var actualType = actual == null ? "null" : actual.GetType().ToString();
            var message = string.Concat(
                $"[<color={COLOR_TAG}>UdonTest</color>] Test <color={COLOR_FAILED}>FAILED!</color>\n",
                $"expected: <color={COLOR_EXPECTED}>{ToDebugString(expected)}</color> ({expectedType})\t",
                $"actual: <color={COLOR_ACTUAL}>{ToDebugString(actual)}</color> ({actualType})");
            Debug.LogError(message, context);
        }

        [RecursiveMethod]
        private static new bool Equals(object objA, object objB)
        {
            if (objA == null)
            {
                return objB == null;
            }
            else if (objB == null)
            {
                return false;
            }

            var objAType = objA.GetType();
            var objBType = objB.GetType();

            if (objAType != objBType)
            {
                return false;
            }

            if (objAType.Name.EndsWith("[]"))
            {
                return Equals((Array)objA, (Array)objB);
            }

            if (objAType == typeof(DataToken))
            {
                return Equals((DataToken)objA, (DataToken)objB);
            }

            if (objAType == typeof(DataList))
            {
                return Equals((DataList)objA, (DataList)objB);
            }

            if (objAType == typeof(DataDictionary))
            {
                return Equals((DataDictionary)objA, (DataDictionary)objB);
            }

            return object.Equals(objA, objB);
        }

        [RecursiveMethod]
        private static bool Equals(Array objA, Array objB)
        {
            if (objA.Length != objB.Length)
            {
                return false;
            }

            for (int i = 0, l = objA.Length; i < l; i++)
            {
                if (!Equals(objA.GetValue(i), objB.GetValue(i)))
                {
                    return false;
                }
            }

            return true;
        }

        [RecursiveMethod]
        private static bool Equals(DataToken objA, DataToken objB)
        {
            if (objA.TokenType != objB.TokenType)
            {
                return false;
            }

            switch (objA.TokenType)
            {
                case TokenType.Null:
                    return true;
                case TokenType.Boolean:
                    return objA.Boolean == objB.Boolean;
                case TokenType.SByte:
                    return objA.SByte == objB.SByte;
                case TokenType.Byte:
                    return objA.Byte == objB.Byte;
                case TokenType.Short:
                    return objA.Short == objB.Short;
                case TokenType.UShort:
                    return objA.UShort == objB.UShort;
                case TokenType.Int:
                    return objA.Int == objB.Int;
                case TokenType.UInt:
                    return objA.UInt == objB.UInt;
                case TokenType.Long:
                    return objA.Long == objB.Long;
                case TokenType.ULong:
                    return objA.ULong == objB.ULong;
                case TokenType.Float:
                    return objA.Float == objB.Float;
                case TokenType.Double:
                    return objA.Double == objB.Double;
                case TokenType.String:
                    return objA.String == objB.String;
                case TokenType.DataList:
                    return Equals(objA.DataList, objB.DataList);
                case TokenType.DataDictionary:
                    return Equals(objA.DataDictionary, objB.DataDictionary);
                case TokenType.Reference:
                    return Equals(objA.Reference, objB.Reference);
                case TokenType.Error:
                    return objA.Error == objB.Error;
                // NOTE: unreachable
                default:
                    return default;
            }
        }

        [RecursiveMethod]
        private static bool Equals(DataList objA, DataList objB)
        {
            if (objA.Count != objB.Count)
            {
                return false;
            }

            for (var i = 0; i < objA.Count; i++)
            {
                if (!Equals(objA[i], objB[i]))
                {
                    return false;
                }
            }

            return true;
        }

        [RecursiveMethod]
        private static bool Equals(DataDictionary objA, DataDictionary objB)
        {
            if (objA.Count != objB.Count)
            {
                return false;
            }

            var keys = objA.GetKeys();
            for (var i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                if (!Equals(objA[key], objB[key]))
                {
                    return false;
                }
            }

            return true;
        }

        [RecursiveMethod]
        private static string ToDebugString(object obj)
        {
            if (obj == null)
            {
                return "null";
            }

            var objType = obj.GetType();

            if (objType.Name.EndsWith("[]"))
            {
                return ToDebugString((Array)obj);
            }

            if (objType == typeof(string))
            {
                return ToDebugString((string)obj);
            }

            if (objType == typeof(DataToken))
            {
                return ToDebugString((DataToken)obj);
            }

            if (objType == typeof(DataList))
            {
                return ToDebugString((DataList)obj);
            }

            if (objType == typeof(DataDictionary))
            {
                return ToDebugString((DataDictionary)obj);
            }

            return obj.ToString();
        }

        private static string ToDebugString(string obj)
        {
            return $"\"{obj}\"";
        }

        [RecursiveMethod]
        private static string ToDebugString(Array obj)
        {
            var array = obj;
            var buf = new string[array.Length];
            for (int i = 0, l = array.Length; i < l; i++)
            {
                buf[i] = ToDebugString(array.GetValue(i));
            }
            return $"[{string.Join(", ", buf)}]";
        }

        [RecursiveMethod]
        private static string ToDebugString(DataToken obj)
        {
            switch (obj.TokenType)
            {
                case TokenType.Null:
                    return "DataToken(null)";
                case TokenType.Boolean:
                case TokenType.SByte:
                case TokenType.Byte:
                case TokenType.Short:
                case TokenType.UShort:
                case TokenType.Int:
                case TokenType.UInt:
                case TokenType.Long:
                case TokenType.ULong:
                case TokenType.Float:
                case TokenType.Double:
                case TokenType.String:
                case TokenType.Reference:
                case TokenType.Error:
                    return $"DataToken({obj})";
                case TokenType.DataList:
                    return $"DataToken({ToDebugString((DataList)obj)})";
                case TokenType.DataDictionary:
                    return $"DataToken({ToDebugString((DataDictionary)obj)})";
                // NOTE: unreachable
                default:
                    return default;
            }
        }

        [RecursiveMethod]
        private static string ToDebugString(DataList obj)
        {
            var list = obj;
            var buf = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                buf[i] = ToDebugString(list[i]);
            }
            return $"DataList([{string.Join(", ", buf)}])";
        }

        [RecursiveMethod]
        private static string ToDebugString(DataDictionary obj)
        {
            var dic = obj;
            var keys = dic.GetKeys();
            var buf = new string[keys.Count];
            for (int i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                var value = dic[key];
                buf[i] = $"{ToDebugString(key)}: {ToDebugString(value)}";
            }
            return $"DataDictionary({{{string.Join(", ", buf)}}})";
        }
    }
}
