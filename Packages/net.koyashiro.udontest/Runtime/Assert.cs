using System;
using UnityEngine;
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
                $"[<color={COLOR_TAG}>UdonTest</color>] Test <color={COLOR_FAILED}>FAILED!</color>\n" +
                $"expected: <color={COLOR_EXPECTED}>{ToDebugString(expected)}</color> ({expectedType})\t" +
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

            if (objAType.Name.EndsWith("[]")
             || objAType == typeof(Array))
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

            if (objType.Name.EndsWith("[]")
             || objType == typeof(Array))
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
