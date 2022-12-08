using System;
using UnityEngine;
using UdonSharp;

namespace Koyashiro.UdonTest
{
    public static class Assert
    {
        private const string COLOR_TAG = "cyan";
        private const string CULOR_OK = "lime";
        private const string COLOR_FAILED = "red";
        private const string COLOR_EXPECTED = "lime";
        private const string COLOR_ACTUAL = "red";

        public static void Equal(object expected, object actual)
        {
            if (!Equals(expected, actual))
            {
                LogFailed(expected, actual);
                return;
            }

            LogOk(expected, actual);
        }

        public static void True(bool actual)
        {
            if (!actual)
            {
                LogFailed(true, actual);
                return;
            }

            LogOk(true, actual);
        }

        public static void False(bool actual)
        {
            if (actual)
            {
                LogFailed(false, actual);
                return;
            }

            LogOk(false, actual);
        }

        public static void Null(object actual)
        {
            if (actual != null)
            {
                LogFailed(null, actual);
                return;
            }

            LogOk(null, actual);
        }

        private static void LogOk(object expected, object actual)
        {
            var expectedType = expected == null ? "null" : expected.GetType().ToString();
            var actualType = actual == null ? "null" : actual.GetType().ToString();
            Debug.Log($"[<color={COLOR_TAG}>UdonTest</color>] Test <color={CULOR_OK}>OK!</color>\nexpected: <color={COLOR_EXPECTED}>{ToDebugString(expected)}</color> ({expectedType})    actual: <color={COLOR_ACTUAL}>{ToDebugString(actual)}</color> ({actualType})");
        }

        private static void LogFailed(object expected, object actual)
        {
            var expectedType = expected == null ? "null" : expected.GetType().ToString();
            var actualType = actual == null ? "null" : actual.GetType().ToString();
            Debug.LogError($"[<color={COLOR_TAG}>UdonTest</color>] Test <color={COLOR_FAILED}>FAILED!</color>\nexpected: <color={COLOR_EXPECTED}>{ToDebugString(expected)}</color> ({expectedType})    actual: <color={COLOR_ACTUAL}>{ToDebugString(actual)}</color> ({actualType})");
        }

        [RecursiveMethod]
        private static new bool Equals(object objA, object objB)
        {
            if (objA == null)
            {
                return objB == null;
            }

            if (objB == null)
            {
                return objA == null;
            }

            var objAType = objA.GetType();
            var objBType = objB.GetType();

            if (objAType != objBType)
            {
                return false;
            }

            if (objAType == typeof(bool[])
             || objAType == typeof(char[])
             || objAType == typeof(sbyte[])
             || objAType == typeof(byte[])
             || objAType == typeof(short[])
             || objAType == typeof(ushort[])
             || objAType == typeof(int[])
             || objAType == typeof(uint[])
             || objAType == typeof(long[])
             || objAType == typeof(ulong[])
             || objAType == typeof(float[])
             || objAType == typeof(double[])
             || objAType == typeof(decimal[])
             || objAType == typeof(string[])
             || objAType == typeof(object[]))
            {
                return Equals((Array)objA, (Array)objB);
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
        private static string ToDebugString(object obj)
        {
            if (obj == null)
            {
                return "null";
            }

            var objType = obj.GetType();

            if (objType == typeof(bool[])
             || objType == typeof(char[])
             || objType == typeof(sbyte[])
             || objType == typeof(byte[])
             || objType == typeof(short[])
             || objType == typeof(ushort[])
             || objType == typeof(int[])
             || objType == typeof(uint[])
             || objType == typeof(long[])
             || objType == typeof(ulong[])
             || objType == typeof(float[])
             || objType == typeof(double[])
             || objType == typeof(decimal[])
             || objType == typeof(string[])
             || objType == typeof(object[]))
            {
                return ToDebugString((Array)obj);
            }

            if (objType == typeof(string))
            {
                return $@"""{obj}""";
            }

            return obj.ToString();
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
    }
}
