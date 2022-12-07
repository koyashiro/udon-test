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
            if (objA == null && objB == null)
            {
                return true;
            }

            if (
                (objA == null && objB != null)
                || (objA != null && objB == null)
            )
            {
                return false;
            }

            var objAType = objA.GetType();
            var objBType = objB.GetType();

            if (objAType != objBType)
            {
                return false;
            }

            if (objAType == typeof(bool[]))
            {
                return Equals((bool[])objA, (bool[])objB);
            }

            if (objAType == typeof(char[]))
            {
                return Equals((char[])objA, (char[])objB);
            }

            if (objAType == typeof(sbyte[]))
            {
                return Equals((sbyte[])objA, (sbyte[])objB);
            }

            if (objAType == typeof(byte[]))
            {
                return Equals((byte[])objA, (byte[])objB);
            }

            if (objAType == typeof(short[]))
            {
                return Equals((short[])objA, (short[])objB);
            }

            if (objAType == typeof(ushort[]))
            {
                return Equals((ushort[])objA, (ushort[])objB);
            }

            if (objAType == typeof(int[]))
            {
                return Equals((int[])objA, (int[])objB);
            }

            if (objAType == typeof(uint[]))
            {
                return Equals((uint[])objA, (uint[])objB);
            }

            if (objAType == typeof(long[]))
            {
                return Equals((long[])objA, (long[])objB);
            }

            if (objAType == typeof(ulong[]))
            {
                return Equals((ulong[])objA, (ulong[])objB);
            }

            if (objAType == typeof(float[]))
            {
                return Equals((float[])objA, (float[])objB);
            }

            if (objAType == typeof(double[]))
            {
                return Equals((double[])objA, (double[])objB);
            }

            if (objAType == typeof(decimal[]))
            {
                return Equals((decimal[])objA, (decimal[])objB);
            }

            if (objAType == typeof(string[]))
            {
                return Equals((string[])objA, (string[])objB);
            }

            if (objAType == typeof(object[]))
            {
                return Equals((object[])objA, (object[])objB);
            }

            return object.Equals(objA, objB);
        }

        [RecursiveMethod]
        private static bool Equals<TA, TB>(TA[] objA, TB[] objB)
        {
            if (objA == null && objB == null)
            {
                return true;
            }

            if (
                (objA == null && objB != null)
                || (objA != null && objB == null)
            )
            {
                return false;
            }

            if (objA.GetType() != objB.GetType())
            {
                return false;
            }

            if (objA.Length != objB.Length)
            {
                return false;
            }

            for (int i = 0, l = objA.Length; i < l; i++)
            {
                if (!Equals(objA[i], objB[i]))
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

            if (objType == typeof(bool[]))
            {
                return ToDebugString((bool[])obj);
            }

            if (objType == typeof(char[]))
            {
                return ToDebugString((char[])obj);
            }

            if (objType == typeof(sbyte[]))
            {
                return ToDebugString((sbyte[])obj);
            }

            if (objType == typeof(byte[]))
            {
                return ToDebugString((byte[])obj);
            }

            if (objType == typeof(short[]))
            {
                return ToDebugString((short[])obj);
            }

            if (objType == typeof(ushort[]))
            {
                return ToDebugString((ushort[])obj);
            }

            if (objType == typeof(int[]))
            {
                return ToDebugString((int[])obj);
            }

            if (objType == typeof(uint[]))
            {
                return ToDebugString((uint[])obj);
            }

            if (objType == typeof(long[]))
            {
                return ToDebugString((long[])obj);
            }

            if (objType == typeof(ulong[]))
            {
                return ToDebugString((ulong[])obj);
            }

            if (objType == typeof(float[]))
            {
                return ToDebugString((float[])obj);
            }

            if (objType == typeof(double[]))
            {
                return ToDebugString((double[])obj);
            }

            if (objType == typeof(decimal[]))
            {
                return ToDebugString((decimal[])obj);
            }

            if (objType == typeof(string[]))
            {
                return ToDebugString((string[])obj);
            }

            if (objType == typeof(object[]))
            {
                return ToDebugString((object[])obj);
            }

            if (objType == typeof(string))
            {
                return $@"""{obj}""";
            }

            return obj.ToString();
        }

        [RecursiveMethod]
        private static string ToDebugString<T>(T[] obj)
        {
            if (obj == null)
            {
                return "null";
            }

            var array = (T[])obj;
            var buf = new string[array.Length];
            for (int i = 0, l = array.Length; i < l; i++)
            {
                buf[i] = ToDebugString(array[i]);
            }
            return $"[{string.Join(", ", buf)}]";
        }
    }
}
